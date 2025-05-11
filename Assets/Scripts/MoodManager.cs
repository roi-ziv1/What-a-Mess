using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;

public class MoodManager : MonoBehaviour
{
    public int max;
    public float currentFill;
    public Image mask;
    public float timeToChangeFill;
    public float fillIncrement;
    public int moodAmountOnPlacedItem;
    
    
    [SerializeField] private Sprite[] moodImages = new Sprite[5];
    [SerializeField] private Image moodImage;
    [SerializeField] private Image fillColor;
    [SerializeField] private Image warningFrame;
    [SerializeField] private GameObject OCDBar;
    // [SerializeField] private Material sheldonOutlineMat;

    // private  Color ogSheldonColor = new Color(59, 62, 75, 255);
    private int _currentMood;
    private float _timeSinceLastFillChange;
    private RectTransform _moodLocation;
    private bool _gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentMood = 4;
        moodImage.sprite = moodImages[_currentMood];
        warningFrame.gameObject.SetActive(false);
        _moodLocation = moodImage.rectTransform;
        mask.fillAmount = currentFill / (float) max;
        UpdateMoodLocation(_moodLocation, mask, mask.fillAmount);
        StartCoroutine(ChangeFill());
        // sheldonOutlineMat.color = ogSheldonColor;
    }

    
    
    private IEnumerator ChangeFill()
    {
        while (true)
        {
            if (GameMaster.Instance.GetIsMomActive())
            {
                currentFill -= fillIncrement;
            }
            currentFill += fillIncrement;
            mask.fillAmount = currentFill / (float) max;
            GetCurrentFill();
            yield return new WaitForSeconds(timeToChangeFill);
        }
    }

    void GetCurrentFill()
    {
        float fillAmount = (float) currentFill / (float) max;
        UpdateMoodLocation(_moodLocation, mask, fillAmount);

        switch (fillAmount)
        {
            case > 0.8f:
                ChangeMood(0);
                warningFrame.gameObject.SetActive(true);
                StartCoroutine(ShakeRoutine());
                // sheldonOutlineMat.color = Color.red;
                break;
          case > 0.6f:
                ChangeMood(1);
                warningFrame.gameObject.SetActive(false);
                StopCoroutine(ShakeRoutine());
                // sheldonOutlineMat.color = ogSheldonColor;
                break;
            case > 0.4f:
                ChangeMood(2);
                break;
            case > 0.2f:
                ChangeMood(3);
                break;
            default:
                ChangeMood(4);
                break;
        }
        
        if(fillAmount >= 1)
        {
            StopCoroutine(ChangeFill());
            GameMaster.Instance.PlayerLost();
            
        }
    }
    
    private void ChangeMood(int mood)
    {
        if(mood == _currentMood) { return; }
        _currentMood = mood;
        moodImage.sprite = moodImages[_currentMood];
        GetCurrentFill();
    }
    
    public void AddMood()
    {
        currentFill -= moodAmountOnPlacedItem;
        if (currentFill < 0)
        {
            currentFill = 0;
        }
        GetCurrentFill();
    }
    
    private void UpdateMoodLocation(RectTransform moodLocation, Image bar, float fillAmount)
    {
        moodLocation.gameObject.SetActive(true);
        
        // Calculate the position at the end of the filled amount
        Vector3[] corners = new Vector3[4];
        bar.rectTransform.GetWorldCorners(corners);

        // Lerp between the left and right corners of the bar
        Vector3 startPosition = corners[2];
        Vector3 endPosition = corners[0];
        Vector3 newPos  = Vector3.Lerp(startPosition, endPosition, fillAmount);
        moodLocation.position = new Vector3(newPos.x, moodLocation.position.y, moodLocation.position.z);
    }

    public void FillDangerFrame(float fillAmount)
    {
        warningFrame.fillAmount = fillAmount;
    }
    
    public void ActivateFrame()
    {
        warningFrame.gameObject.SetActive(true);
    }
    
    public void DeactivateFrame()
    {
        warningFrame.gameObject.SetActive(false);
    }
    
    private IEnumerator ShakeRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        while (true)
        {
            ShakeBar();
            yield return wait;
        }
    }
    
    private void ShakeBar()
    {
        transform.DOShakePosition(1, 0.5f, 10, 90, false, true);
    }

    private IEnumerator AddFill(float fillTarget)
    {
        while (mask.fillAmount < fillTarget)
        {
            mask.fillAmount += 0.001f;
            yield return new WaitForSeconds(0.001f);
        }
        yield break;
    }
    
    private IEnumerator RemoveFill(float fillTarget)
    {
        while (mask.fillAmount > fillTarget)
        {
            mask.fillAmount -= 0.001f;
            yield return new WaitForSeconds(0.001f);
        }
        yield break;
    }
    
}
