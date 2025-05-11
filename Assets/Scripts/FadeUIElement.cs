using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUIElement : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    

    private void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn());
    }
    
    private IEnumerator FadeIn()
    {
        for (float i = 0; i <= 1; i += 0.05f)
        {
            _canvasGroup.alpha = i;
            yield return new WaitForSeconds(0.01f);
        }
        _canvasGroup.alpha = 1;
    }
}
