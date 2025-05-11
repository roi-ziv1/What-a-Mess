using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private int _timeLeft = 120;
    [SerializeField] private TextMeshProUGUI itemsLeftText;
    [SerializeField] private Image incomingImage;
    [SerializeField] private Image inDangerImage;
    [SerializeField] private Image safeImage;
    private int _minutesLeft;
    private int _secondsLeft;
    private bool _toCountDown;
    private float _sinceLastUpdate = 0;
    private int _maxItems = 5;
    private int _currentItems = 0;
    private bool _dropAlpha;
    private bool _toFlashText;

    private float _momTimerRoll;
    private float _curMomTimer;
    public int momTimerMax;
    public int momTimerMin;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCountDown();
        _maxItems = GameMaster.Instance.GetMaxItems();
        _momTimerRoll = Random.Range(momTimerMin, momTimerMax);
        _curMomTimer = _momTimerRoll;
        itemsLeftText.text = _currentItems + "/" + _maxItems;
    }
    
    public void StartCountDown()
    { 
        _toCountDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_toCountDown)
        {
            _momTimerRoll -= Time.deltaTime;
        }
        if (_momTimerRoll <= 0)
        {
            RollMomTimer();
            _toCountDown = false;
            // incomingImage.gameObject.SetActive(true);
            _toFlashText = true;
            GameMaster.Instance.ActivateSafeZones();
            Invoke(nameof(ActivateMom) , 5f);
            Invoke(nameof(DisableMom), 10f);
        }
    }
    
    private void RollMomTimer()
    {
        _momTimerRoll = Random.Range(momTimerMin, momTimerMax);
        _curMomTimer = _momTimerRoll;
    }
    
    
    private void ActivateMom()
    {
        GameMaster.Instance.ActivateMom();
        // incomingImage.gameObject.SetActive(false);
    }
    
    private void DisableMom()
    {
        GameMaster.Instance.DisableMom();
        GameMaster.Instance.DeactivateSafeZones();
        _toCountDown = true;
    }
    
    public void AddItem()
    {
        _currentItems++;
        itemsLeftText.text = _currentItems + "/" + _maxItems;
        if (_currentItems == _maxItems)
        {
            _toCountDown = false;
            GameMaster.Instance.PlayerWon();
        }
    }

    public void InDanger()
    {
        // inDangerImage.gameObject.SetActive(true);
        // safeImage.gameObject.SetActive(false);
    }
    
    public void Safe()
    {
        // safeImage.gameObject.SetActive(true);
        // inDangerImage.gameObject.SetActive(false);
    }

    public void DisableDangerIndicators()
    {
        safeImage.gameObject.SetActive(false);
        inDangerImage.gameObject.SetActive(false);
    }
    
    // public void PlayerCaught()
    // {
    //     caughtText.enabled = true;
    //     _toCountDown = false;
    //     itemsLeftText.color = Color.red;
    // }
}
