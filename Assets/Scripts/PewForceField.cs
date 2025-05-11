using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PewForceField : MonoBehaviour
{
    [SerializeField] private GameObject meter;
    private Vector3 _meterPos;
    // Start is called before the first frame update
    void Start()
    {
        _meterPos = meter.transform.position;
        if (Camera.main != null)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(_meterPos);
            transform.position = newPos;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(_meterPos);
        transform.position = newPos;
    }
}
