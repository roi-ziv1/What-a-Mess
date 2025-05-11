using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator _anim;
    
    private readonly int _restart = Animator.StringToHash("Restart");
    private Vector3 _pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(this.transform.position + mainCamera.transform.rotation * Vector3.forward,mainCamera.transform.rotation * Vector3.up);
    }

    private void OnEnable()
    {
        _anim.SetTrigger(_restart);
    }
}
