using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class EndingScript : MonoBehaviour
{
    private int menuSceneNum=0;
    public InputAction menu;
    [SerializeField] private Animator fadeAnim;
    public void Awake()
    {
        if (menu== null)
        {
            Debug.LogError("menu action is not set in the Inspector");
        }
    }
    public void BackToMenu(InputAction.CallbackContext callbackContext)
    {
        StartCoroutine(StartFade());
    }
   
    private IEnumerator StartFade()
    {
        fadeAnim.SetBool("Fade",true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(0);
        //fadeAnim.SetBool("Fade",false);
    }
    public void OnEnable()
    {
        menu.Enable();
        menu.performed += BackToMenu;
    }
     
    public void OnDisable()
    {
        menu.performed -= BackToMenu;
        menu.Disable(); 
    }
}

