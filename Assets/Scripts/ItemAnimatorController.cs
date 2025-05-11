
using System.Collections;

using UnityEngine;
using DG.Tweening;


public class ItemAnimatorController : MonoBehaviour
{
    public Animator animator;
    private static readonly int TouchesHash = Animator.StringToHash("Touches");
    private static readonly int MekupalHash = Animator.StringToHash("mekupal");
    public float startinghigh;
    private bool _onItem;
    public bool toShake;
    private Vector3 _shakeAxis = new Vector3(0.05f, 0, 0.05f);
    public Collider _collider;
    public GameObject itemChild;
    [SerializeField] private bool isTshirt;
    public bool tshirtAnimation;
    public void Awake()
    {
        startinghigh = transform.position.y;
    }
    
    void Start()
    {
        _collider = GetComponent<Collider>();
    }
    
    public void StartAnimation(bool touchitem)
    {
        if (touchitem) return;
        animator.SetBool(TouchesHash,true);
        // toShake = true;
        // StartCoroutine(ShakeRoutine());
        _collider.enabled = true;
    }

    public void EndAnimation()
    {
        if (isTshirt)
        {
            animator.SetBool(MekupalHash,tshirtAnimation);
        }
        animator.SetBool(TouchesHash,false);
        
        if (_onItem)
        {
            transform.position=new Vector3(transform.position.x,startinghigh,transform.position.z+3);
        }
        else
        {
            transform.position=new Vector3(transform.position.x,startinghigh,transform.position.z);
        }
        // toShake = false;
        // StopCoroutine(ShakeRoutine());
        _collider.enabled = false;
    }

    
    IEnumerator ShakeRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        while (toShake)
        {
            Shake();
            yield return wait;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            _onItem = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            _onItem = false;
        }

        // if (other.gameObject.CompareTag("Item"))
        // {
        //     EndAnimation();
        // }
    }

    private void Update()
    {
        if (GameMaster.Instance.isCarrying)
        {
            itemChild.layer = 0;
        }
        else
        {
            itemChild.layer = 3;
        }
    }

    private void Shake()
    {
        print("test");
        transform.DOShakePosition(1f, _shakeAxis,10,90,false,true);
    }
    
    
    
}
