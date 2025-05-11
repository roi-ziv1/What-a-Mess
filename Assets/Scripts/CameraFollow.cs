using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _offset;
    private Vector3 _newpos;
    
    private Camera _camera;
    [SerializeField] private GameObject player;

    [SerializeField] private float cameraMaxX;
    [SerializeField] private float cameraMinX;
    [SerializeField] private float cameraMaxZ;
    [SerializeField] private float cameraMinZ;

    private float _cameraX;
    private float _cameraZ;
    
    void Start()
    {
        _offset = player.transform.position - transform.position;
    }

    void CameraLimits()
    {
        if (player.transform.position.x>cameraMaxX)
        {
            _cameraX = cameraMaxX;
        }
        else if (player.transform.position.x<cameraMinX)
        {
            _cameraX = cameraMinX;
        }
        else
        {
            _cameraX = player.transform.position.x;
        }
        if (player.transform.position.z>cameraMaxZ)
        {
            _cameraZ = cameraMaxZ;
        }
        else if (player.transform.position.z<cameraMinZ)
        {
            _cameraZ = cameraMinZ;
        }
        else
        {
            _cameraZ = player.transform.position.z;
        }
    }
    void Update()
    {
        if(GameMaster.Instance._isGameOver)
        {
            transform.position = new Vector3(player.transform.position.x-_offset.x ,transform.position.y
                ,player.transform.position.z-_offset.z);
        }
        else
        {
            CameraLimits();
            _newpos = transform.position;
            _newpos.x = _cameraX - _offset.x;
            _newpos.z= _cameraZ - _offset.z;
            transform.position = _newpos;
        }
        
    }
}
