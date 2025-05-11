using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;


public class IntroController : MonoBehaviour
{
    public int sceneNum;
    private int _clickNumber=2;
    public Image image1;
    
    public void PressButton()
    {
        image1.enabled = false;
        _clickNumber--;
        if (_clickNumber == 0)
        {
            SceneManager.LoadScene(sceneNum);
        }
        
    }
}
