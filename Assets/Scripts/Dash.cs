using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    private PlayerInputAction _dash;
    private bool _canDash= true;
    private bool _isDashing;
    private Rigidbody _rbdash;
    
    [SerializeField] private float dashingPower =10f;
    [SerializeField] private float waitTime = 2f;

    private void Awake()
    {
        _dash = new PlayerInputAction();
        _dash.Player.Dash.performed +=x=> PlayerDash();
       
    }
    private void Start()
    {
        _rbdash = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isDashing)
        {
            Vector3 dashForce = transform.forward * dashingPower;
            _rbdash.AddForce(dashForce, ForceMode.VelocityChange);
        }
    }

    private void PlayerDash()
    {
        if (_canDash)
        {
            StartCoroutine(StartDash());
        }
    }
    private IEnumerator StartDash()
    {
        _canDash = false;
        _isDashing = true;
        yield return new WaitForSeconds(waitTime*0.1f);
        _isDashing = false;
        yield return new WaitForSeconds(waitTime);
        _canDash = true;
    }
    
    private void OnEnable()
    {
        _dash.Enable();
    }
     
    private void OnDisable()
    {
        _dash.Disable(); 
    }
    
}
