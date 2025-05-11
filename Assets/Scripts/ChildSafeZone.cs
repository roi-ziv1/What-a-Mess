using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSafeZone : MonoBehaviour
{
    //public BoxCollider _collider;
    public FadeSafeZone safeZonePressed;
    public FadeSafeZone safeZoneOn;
    private bool _isOnZone;
    public List<BoxCollider> _collider;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _collider.Count; i++)
        {
           _collider[i].enabled = false;
        }
        
        safeZoneOn.FadeOut();
        safeZonePressed.FadeOut();
    }
    public void ActivateChildSafeZone()
    {
        for (int i = 0; i < _collider.Count; i++)
        {
            _collider[i].enabled = true;
        }
        safeZoneOn.gameObject.SetActive(true);
    }
    
    public void DeactivateChildSafeZone()
    {
        for (int i = 0; i < _collider.Count; i++)
        {
            _collider[i].enabled = false;
        }
        safeZoneOn.FadeOut();
        safeZonePressed.FadeOut();
        GameMaster.Instance.canLying = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        safeZoneOn.gameObject.SetActive(false);
        safeZonePressed.gameObject.SetActive(true);
        GameMaster.Instance.canLying = true;
        // GameMaster.Instance.MakeSafe();
        _isOnZone = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        safeZoneOn.gameObject.SetActive(true);
        safeZonePressed.FadeOut();
        GameMaster.Instance.MakeUnsafe();
        _isOnZone = false;
    }

    private void Update()
    {
        if (_isOnZone)
        {
            if (GameMaster.Instance.canLying)
            {
                GameMaster.Instance.MakeSafe();
            }
            else
            {
                GameMaster.Instance.MakeUnsafe();
            }
        }
    }
}
