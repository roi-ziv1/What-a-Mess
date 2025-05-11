using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SheldomAnimation : MonoBehaviour
{
    private Rigidbody _sheldonrigidbody;
    public Animator animator;
    private bool _run;
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int CarryHash = Animator.StringToHash("Carry");
    private static readonly int LyingDownHash = Animator.StringToHash("LyingDown");
    //private static readonly int LyingUpHash = Animator.StringToHash("Dance");
    private Vector3 StartingY;
    private bool isLying;
    private bool _canLying;
    void Start()
    {
        _sheldonrigidbody = GetComponentInParent<Rigidbody>();
        animator.GetComponent<Animator>();
        StartingY = transform.localPosition;
    }
    
    void Update()
    {
        _run = ((_sheldonrigidbody.velocity.x <= 0.1)&&(_sheldonrigidbody.velocity.x >= -0.1)) || ((_sheldonrigidbody.velocity.z <= 0.1&&_sheldonrigidbody.velocity.z >= -0.1));
        animator.SetBool(SpeedHash,!_run);
        _canLying = GameMaster.Instance.canLying;
        if (_canLying!=isLying && _run)
        {
            if (_canLying)
            {
                transform.DOLocalMoveY(StartingY.y-0.2f,0.35f).SetEase(Ease.InCubic);
                GameMaster.Instance.playerSpeed = 0f;
            }
            else
            {
              transform.DOLocalMoveY(StartingY.y,0.0002f).SetEase(Ease.Linear);  
              GameMaster.Instance.playerSpeed = 5f;
            }

            isLying = _canLying;
        }
        animator.SetBool(CarryHash,GameMaster.Instance.isCarrying); 
        animator.SetBool(LyingDownHash,_canLying);
       
       
        // else
        // {
        //     animator.SetBool(SpeedHash,false);
        // }
        
        
    }

    
}
