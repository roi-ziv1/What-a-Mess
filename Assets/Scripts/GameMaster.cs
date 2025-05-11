using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameObject[] items = new GameObject[5];
    [SerializeField] private GameObject[] itemLocations = new GameObject[5];
    [SerializeField] private GameObject[] placedItems = new GameObject[5];

    private Vector3[] _startingPositions;
    
    [SerializeField] private SafeZone safeZones;
    
    [SerializeField] private MoodManager moodManager;
    
    [SerializeField] UIManager uiManager;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private MomAnimationController momAnimationController;
    public Animator faceAnimator;
    
    [SerializeField] private SlowDownEffect slowDownEffect;
    [SerializeField] private GameObject eyeIcon;
    
    public bool isCarrying;
    public bool canLying;
    public bool toDance;
    public ParticleSystem pewParticles;
    public ParticleSystem outlineParticles;
    public bool _isGameOver;
    public bool TutorialOff;
    private Dictionary<GameObject, GameObject> _locationPairs;
    private Dictionary<GameObject, GameObject> _placedPairs;
    
    public static GameMaster Instance;
    private static readonly int Open = Animator.StringToHash("Open");

    private bool _isSafe;
    public bool _isMomActive;
    
    public float playerSpeed = 7f;
    public float detectionTime = 3.2f;
    private float _timeSeen = 0;
    

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one GameMaster instance");
            return;
        }
        
        Instance = this;
    }

    private void Start()
    {
        _locationPairs = new Dictionary<GameObject, GameObject>();
    
        for (int i = 0; i < items.Length; i++)
        {
            _locationPairs.Add(items[i], itemLocations[i]);
        }
        
        _placedPairs = new Dictionary<GameObject, GameObject>();
        for (int i = 0; i < items.Length; i++)
        {
            _placedPairs.Add(items[i], placedItems[i]);
        }
        
        _startingPositions = new Vector3[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            _startingPositions[i] = items[i].transform.position;
        }
        
        Shuffle(_startingPositions);
        
        for (int i = 0; i < items.Length; i++)
        {
            items[i].transform.position= new Vector3(_startingPositions[i].x,items[i].transform.position.y,_startingPositions[i].z);
        }
        
        _isSafe = false;
        outlineParticles.Stop();
        eyeIcon.SetActive(false);
    }

    
    void Shuffle(Vector3[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            Vector3 temp = list[i];
            int randomIndex = Random.Range(i, list.Length);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    
    public Vector3 GetLocation(GameObject item)
    {
        if(_locationPairs.TryGetValue(item, out var pair))
        {
            return pair.transform.position;
        }

        return Vector3.zero;
    }

    public void ActivateTarget(GameObject item)
    {
        if (_locationPairs.TryGetValue(item, out var pair))
        {
            audioManager.pickItemAudio.Play();
            pair.SetActive(true);
            pair.layer = 3;
            outlineParticles.transform.position = pair.transform.position;
            outlineParticles.Play();
        }
    }
    
    public void DeactivateTarget(GameObject item)
    {
        if (_locationPairs.TryGetValue(item, out var pair))
        {
            audioManager.dropItemAudio.Play();
            pair.layer = 0;
            pair.SetActive(false);
            outlineParticles.Stop();
        }
    }

    private void ItemPlaced()
    {
        faceAnimator.SetTrigger("happy");
        uiManager.AddItem();
        moodManager.AddMood();
        audioManager.itemPlacedAudio.Play();
        audioManager.HappySound();
        
    }
    
    public int GetMaxItems()
    {
        return items.Length;
    }

    public void PlaceItem(GameObject item)
    {
        if (!_placedPairs.TryGetValue(item, out var pair)) return;
        pair.SetActive(true);
        pewParticles.transform.position = pair.transform.position;
        pewParticles.Play();
        ItemPlaced();
    }

    public void ActivateMom()
    {
        faceAnimator.SetBool("scared",true);
        _isMomActive = true;
        audioManager.openDoor.Play();
        momAnimationController.StartMomAnimation();
        doorAnimator.SetBool(Open, true);
        audioManager.stepAudio.Stop();
        StartCoroutine(SafeZoneRoutine());
        // Invoke(nameof(StartMomSearch), 1f);
    }

    public void StartMomSearch()
    {
        StartCoroutine(SafeZoneRoutine());
    }
    
    public void DisableMom()
    {
        faceAnimator.SetBool("scared",false);
        _isMomActive = false;
        doorAnimator.SetBool(Open, false);
        audioManager.closeDoor.Play();
        StopCoroutine(SafeZoneRoutine());
        uiManager.DisableDangerIndicators();
    }

    public void PlayerWon()
    {
        _isGameOver = true;
        playerSpeed = 0f;
        toDance = true;
        faceAnimator.SetTrigger("happyWin");
        StartCoroutine(EndLevel("Dance","EndingScene"));
    }

    public void PlayerLost()
    {
        if (!_isGameOver)
        {
            _isGameOver= true;
            playerSpeed = 0f;
            StartCoroutine(EndLevel("Angry","LossScene explode"));
        }
        
        
    }
    public void PlayerCaught()
    {
        if(!_isGameOver)
        {
           _isGameOver = true;
            playerSpeed = 0f;
            StartCoroutine(EndLevel("Loss","LossScene")); 
        }
        
    }
    
    IEnumerator EndLevel(string animationName,string sceneName)
    {
        slowDownEffect.ZoomAndLoad(animationName);
        yield return new WaitForSeconds(5);
        ChangeLevel(sceneName); 
    }

    
    public void MakeSafe()
    {
        _isSafe = true;
        uiManager.Safe();

    }
    
    public void MakeUnsafe()
    {
        _isSafe = false;
        uiManager.InDanger();
    }
    
    private IEnumerator SafeZoneRoutine()
    {
        yield return new WaitForSeconds(1f);
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        eyeIcon.SetActive(true);
        _timeSeen = 0;
        while (true)
        {
            yield return wait;
            if (!_isMomActive) continue;
            if (!_isSafe)
            {
                _timeSeen += 0.2f;
                moodManager.FillDangerFrame(_timeSeen / detectionTime);
                if (_timeSeen >= detectionTime)
                {
                    PlayerCaught();
                }
            }
            else if(_isSafe)
            {
                _timeSeen = 0;
                eyeIcon.SetActive(false);
            }

        }
    }
    
    public void ActivateSafeZones()
    {
        audioManager.safezoneAudio.Play();
        audioManager.stepAudio.Play();
        safeZones.ActivateSafeZone();
    }
    
    public void DeactivateSafeZones()
    {
        audioManager.safezoneAudio.Stop();
        safeZones.DeactivateSafeZone();
    }
    
    public bool GetGameOver()
    {
        return _isGameOver;
    }

    public void ChangeLevel(string sceneName)
    {
        StartCoroutine(StartFadeAnimation(sceneName));
    }
    
    public IEnumerator StartFadeAnimation(string sceneName)
    {
        fadeAnimator.SetBool("Fade",true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneName);
    }
    
    public bool GetIsMomActive()
    {
        return _isMomActive;
    }
}
