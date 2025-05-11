using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public PlayerInputAction Pause;
    public Image pauseImage;

    private void Awake()
    {
        Pause = new PlayerInputAction();
        Pause.Player.Pause.performed += x => PressPause();
    }

    void PressPause()
    {
        if (!GameMaster.Instance.TutorialOff) return;
        pauseImage.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pauseImage.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
       Time.timeScale = 1;
       GameMaster.Instance.ChangeLevel("SHLAV 1");
    }
    
    public void Menu()
    {
        Time.timeScale = 1;
        GameMaster.Instance.ChangeLevel("OpenScene");
        
    }
    
    private void OnEnable()
    {
        Pause.Player.Enable();
    }

    private void OnDisable()
    {
        Pause.Player.Disable();
    }
}
