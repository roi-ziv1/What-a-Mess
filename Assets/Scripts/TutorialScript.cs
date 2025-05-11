using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialScript : MonoBehaviour
{
    public InputAction pressA;
    [SerializeField] private GameObject tutorialGameObject;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject OCDBar;
    [SerializeField] private GameObject progress;
    private void Awake()
    {
        if (pressA == null)
        {
            Debug.Log("pressA not found");
        }
        Time.timeScale = 0f;
    }

    private void SkipTutorial(InputAction.CallbackContext callbackContext)
    {
        Transform tutorialChild= tutorialGameObject.transform.Find("frame");
        if (tutorialChild != null)
        {
            GameObject childObject = tutorialChild.gameObject;
            childObject.SetActive(false);
        }
        StartCoroutine(StartAnimation());
        
    }

    IEnumerator StartAnimation()
    {
        playerAnimator.SetTrigger("Meltdown");
        GameMaster.Instance.faceAnimator.SetTrigger("angry");
        float animationLength = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animationLength);
        Time.timeScale = 1f;
        tutorialGameObject.SetActive(false);
        OCDBar.gameObject.SetActive(true);
        progress.gameObject.SetActive(true);
        GameMaster.Instance.TutorialOff = true;
    }
    private void OnEnable()
    {
        pressA.Enable();
        pressA.performed += SkipTutorial;
    }

    private void OnDisable()
    {
        pressA.Disable();
        pressA.performed -= SkipTutorial;
    }
}
