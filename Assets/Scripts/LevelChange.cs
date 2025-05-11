using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChange : MonoBehaviour
{   
    public InputAction pick;
    private bool _aPress;
    [SerializeField] private Animator fadeAnim;
    
    public virtual void Awake()
    {
        if (pick== null)
        {
            Debug.LogError("Pick action is not set in the Inspector");
        }
    }
    
   
    public void PressButton(InputAction.CallbackContext callbackContext)
    {
            StartCoroutine(StartFade());
    }

    private IEnumerator StartFade()
    {
        fadeAnim.SetBool("Fade",true);
        yield return new WaitForSeconds(1f);
       SceneManager.LoadSceneAsync("SHLAV 1");
    }
    
    public virtual void OnEnable()
    {
        pick.Enable();
        pick.performed+=PressButton;
       
    }
     
    public virtual void OnDisable()
    { 
        pick.performed-= PressButton;
        pick.Disable();
    }
    
   
}
