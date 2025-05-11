using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimatorController : MonoBehaviour
{

    public Animator faceAnimator;

   
    void Update()
    {
        if (GameMaster.Instance._isMomActive)
        {
            faceAnimator.SetBool("scared",true);
        }
        else
        {
            faceAnimator.SetBool("scared",false);
        }

        
    }
}
