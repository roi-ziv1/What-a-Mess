
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector2 _currentMovement;
    //private Vector3 _directionVector;
    private Rigidbody _rb;
    private float playerSpeed;
   // private PlayerInputAction _lyingDown;

   // private void Awake()
   // {
       // _lyingDown=new PlayerInputAction();
      //  _lyingDown.Player.LyingDown.performed += x => SheldonLying();
   // }

    void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        playerSpeed = GameMaster.Instance.playerSpeed;
        //_rb.constraints=RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        if(GameMaster.Instance._isGameOver)return;
        Rotation();
        MovePlayer();
       // SheldonLying();
        playerSpeed = GameMaster.Instance.playerSpeed;
    }

    // private void SheldonLying()
    // {
    //     if (GameMaster.Instance.canLying)
    //     {
    //         playerSpeed = 0.3f;
    //     }
    //     else
    //     {
    //         //GameMaster.Instance.canLying = false;
    //         playerSpeed = 5f;
    //         
    //     }
    // }
    
    private void Rotation()
    {
        //Vector3 currentPosition = transform.position;
        if (_currentMovement != Vector2.zero)
        {
            Vector3 newPosition =
                cameraTransform.TransformDirection(new Vector3(_currentMovement.x, 0, _currentMovement.y));
            newPosition.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(newPosition.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        //Vector3 positionToLookAt = currentPosition +cameraTransform.TransformDirection(newPosition.normalized);
        //transform.LookAt(positionToLookAt);
    }

    private void OnMove(InputValue value)
    {
        _currentMovement = value.Get<Vector2>();
    }
    
    void MovePlayer()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        Vector3 moveDirection1 = (right * _currentMovement.x + forward * _currentMovement.y).normalized;
        moveDirection1.y = 0; 
        _rb.velocity = moveDirection1 * playerSpeed;
    }
    
    //private void OnEnable()
   // {
    //     _lyingDown.Enable();
    //}
     
   // private void OnDisable()
   // {
    //    _lyingDown.Disable(); 
   // }
    
   // private void OnDestroy()
   // {
    //    _lyingDown.Dispose();
   // }
}
