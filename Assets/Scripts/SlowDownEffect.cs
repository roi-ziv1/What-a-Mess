using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SlowDownEffect : MonoBehaviour
{
    public string sceneToLoad;
    
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Animator playerAnimator;
    
    public void ZoomAndLoad(string animationName)
    {
        StartCoroutine(ZoomAndLoadEnum(animationName));
    }

    IEnumerator ZoomAndLoadEnum(string animationName)
    { 
        gameCamera.DOOrthoSize(2.65f, 0.2f).SetEase(Ease.InOutQuart);
        playerGameObject.transform.DORotate(new Vector3(0, 220f, 0), 0.1f).SetEase(Ease.InOutQuart);
       
        yield return new WaitForSeconds(0.5f);
         if (animationName == "Angry")
         {
             GameMaster.Instance.faceAnimator.SetTrigger("angryloss");
         }
        playerAnimator.SetTrigger(animationName);
       
    }
   
    // public void SlowdownAndLoad(string sceneName)
    // {
    //     sceneToLoad = sceneName;
    //     GameMaster.Instance.ChangeLevel(sceneToLoad);
    // }
    
}
