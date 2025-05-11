using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    private bool _canCarry;
    //private bool _isCarrying;
    private bool _touchItem;
    private GameObject _currItem;
    //private Vector3 _lookTarget = Vector3.zero;
    [SerializeField] private GameObject box;
    public PlayerInputAction pick;
    private Gamepad _gamepad = Gamepad.current;
    [Header("Vibration")] [SerializeField] private float lowFrequency;
    [SerializeField] private float highFrequency, duration;
    
    

   private void Awake()
    {
        pick = new PlayerInputAction();
        pick.Player.PickUp.performed +=x=> PickUp();
       
    }

    private void Update()
    {
        
        if (GameMaster.Instance.isCarrying)
        {//if(_currItem==null)return;
            _currItem.transform.position = box.transform.position;
            _currItem.transform.rotation = box.transform.rotation;
            if(GameMaster.Instance.canLying){DropItem();}
        }
    }

    private void PickUp()
    {
        if(Time.timeScale==0) return;
        if (_canCarry&&!GameMaster.Instance.canLying)
        {
            PickupItem();
        }
        else
        {
            DropItem();
        }
        
    }
        
    public void PickupItem()
    {
        if (!GameMaster.Instance.isCarrying && _canCarry)
        {
            GameMaster.Instance.playerSpeed = 5;
            // _currItem.transform.SetParent(transform);
            // _currItem.transform.position = box.transform.position;
            GameMaster.Instance.isCarrying = true;
            _canCarry = false;
            ItemAnimatorController itemAnimatorController = _currItem.GetComponent<ItemAnimatorController>();
            //itemAnimatorController.tshirtAnimation = true;
            itemAnimatorController.EndAnimation();
            itemAnimatorController._collider.enabled=false;
            GameMaster.Instance.ActivateTarget(_currItem);
            
        }
    }
    
    private void DropItem()
    {
        if (_canCarry || !GameMaster.Instance.isCarrying) return;
        // _currItem.transform.SetParent(null);
        GameMaster.Instance.playerSpeed = 7;
        Vector3 target = GameMaster.Instance.GetLocation(_currItem);
        GameMaster.Instance.DeactivateTarget(_currItem);
        if(Vector3.Distance(transform.position, target) < 5)
        {
            GameMaster.Instance.PlaceItem(_currItem);
            GameMaster.Instance.isCarrying = false;
            _currItem.SetActive(false);
            _currItem = null;
            _touchItem = false;
            
        }
        else
        {
            ItemAnimatorController itemAnimatorController = _currItem.GetComponent<ItemAnimatorController>();
            itemAnimatorController.tshirtAnimation = false;
            itemAnimatorController._collider.enabled=true;
            itemAnimatorController.EndAnimation();
            GameMaster.Instance.isCarrying = false;
            _currItem = null;
            _touchItem = false;
            Vibrate();
        }
        

    }
    
    private void OnTriggerEnter(Collider coll)
    {
        if (!coll.gameObject.CompareTag("Item") || GameMaster.Instance.isCarrying||  GameMaster.Instance.canLying) return;
        _currItem = coll.transform.parent.gameObject;
        _canCarry = true;
        ItemAnimatorController itemAnimatorController=_currItem.GetComponent<ItemAnimatorController>();
        itemAnimatorController.StartAnimation(_touchItem);
        itemAnimatorController.tshirtAnimation = true;
         _touchItem = true;
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Item")&& !GameMaster.Instance.isCarrying)
        {
            _canCarry = false;
            if(_touchItem)
            {  ItemAnimatorController itemAnimatorController = _currItem.GetComponent<ItemAnimatorController>();
                itemAnimatorController.tshirtAnimation = false;
                itemAnimatorController._collider.enabled=true;
                itemAnimatorController.EndAnimation();
                _touchItem = false;
            }

            
        }
    }
    
    private void Vibrate()
    {
        _gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
        Invoke(nameof(StopVibration),duration);
    }

    private void StopVibration()
    {
        _gamepad.SetMotorSpeeds(0, 0);
    }
    
     private void OnEnable()
     {
         pick.Enable();
     }
     
     private void OnDisable()
     {
         pick.Disable(); 
     }
 }
