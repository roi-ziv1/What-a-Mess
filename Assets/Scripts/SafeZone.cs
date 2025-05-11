using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private List<ChildSafeZone> safeZoneList;
    [SerializeField] private GameObject RedZone;
    //[SerializeField] private List<GameObject> safeZonePressedList;
    //public List<BoxCollider> colliderList;
    public ChildSafeZone SafeZone0, SafeZone1, SafeZone2;
    //private BoxCollider _collider;
    //private GameObject safeZonePressed;
    private ChildSafeZone safeZone;
   // private bool _isOnZone;

    private int randomZone;
    // Start is called before the first frame update
    
    void Start()
    {
        safeZoneList[0] = SafeZone0;
        safeZoneList[1] = SafeZone1;
        safeZoneList[2] = SafeZone2;
        //     int randomZone = Random.Range(0, safeZoneOnList.Count);
        //     safeZonePressed = safeZonePressedList[randomZone];
        //     safeZoneOn = safeZoneOnList[randomZone];
        //     _collider = colliderList[randomZone];
        //     _collider.enabled = false;
        //     safeZoneOn.SetActive(false);
        //     safeZonePressed.SetActive(false);
    }
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!other.CompareTag("Player")) return;
    //     safeZoneOn.SetActive(false);
    //     safeZonePressed.SetActive(true);
    //     GameMaster.Instance.canLying = true;
    //     // GameMaster.Instance.MakeSafe();
    //     _isOnZone = true;
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (!other.CompareTag("Player")) return;
    //     safeZoneOn.SetActive(true);
    //     safeZonePressed.SetActive(false);
    //     GameMaster.Instance.MakeUnsafe();
    //     _isOnZone = false;
    // }
    
    public void ActivateSafeZone()
    {
        randomZone = Random.Range(0, 2);
        safeZone = safeZoneList[randomZone];
        RedZone.SetActive(true);
        safeZone.ActivateChildSafeZone();
       // safeZonePressed = safeZonePressedList[randomZone];
        // safeZoneOn = safeZoneList[randomZone];
        // _collider = colliderList[randomZone];
        // _collider.enabled = true;
        // safeZoneOn.SetActive(true); 
    }
    
    public void DeactivateSafeZone()
    {
        safeZone.DeactivateChildSafeZone();
        GameMaster.Instance.canLying = false;
        RedZone.SetActive(false);
         // safeZoneOn.SetActive(false);
         // safeZonePressed.SetActive(false);
        // _collider.enabled = false;
    }

    // private void Update()
    // {
    //     if (_isOnZone)
    //     {
    //         if (GameMaster.Instance.canLying)
    //         {
    //             GameMaster.Instance.MakeSafe();
    //         }
    //         else
    //         {
    //             GameMaster.Instance.MakeUnsafe();
    //         }
    //     }
    // }
}
