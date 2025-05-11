using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomAnimationController : MonoBehaviour
{
    [SerializeField] private Animator momAnimator;
    [SerializeField] private Animator legsAnimator;
    private static readonly int hideHash = Animator.StringToHash("Hide");

    [SerializeField] private float waitingTime;
    public void StartMomAnimation()
    {
        StartCoroutine(MomIenumerator());
    }

    public IEnumerator MomIenumerator()
    {
        yield return new WaitForSeconds(waitingTime);
        momAnimator.SetTrigger(hideHash);
        legsAnimator.SetTrigger(hideHash);
    }

}
