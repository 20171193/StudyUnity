using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class TestMoveTank : MonoBehaviour
{
    public Camera mainCamera;

    public Rigidbody myRb;
    public BoxCollider myBcol;

    public Transform tankBodyTransform;

    public float moveSpeed;       // 이동속도

    public float jumpPower = 5.0f;      // 점프력
    public float jumpCooltime = 2.0f;   // 점프 쿨타임
    Coroutine jumpCoolTimeCor = null;

    [SerializeField]
    bool isFlying;
    [SerializeField]
    bool isJumping;

    [SerializeField]
    Vector3 moveDirection;

    const int Floor_Layer = 7;

    private void Awake()
    {
        isFlying = false;
        moveDirection = myRb.transform.forward;
        moveSpeed = myRb.mass * 10f;
    }

    #region Collision Fucntion
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌");
        if(collision.gameObject.tag == "Floor")
        {
            if (jumpCoolTimeCor != null)
                StopCoroutine(jumpCoolTimeCor);
            isFlying = false;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
    }
    #endregion

    private Vector3 SetDirection() // Setting Move Direction
    {
        float inputVTC = Input.GetAxis("Vertical");
        return tankBodyTransform.forward * inputVTC;
    }
    private void Movement()     // Move
    {
        myRb.AddForce(tankBodyTransform.forward * moveSpeed /* Time.deltaTime*/, ForceMode.Force);
    }
    private void Jumping()         // Start Jump
    {
        if(!isFlying && isJumping)
        {
            isJumping = false;
            isFlying = true;
            myRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            StartCoroutine(JumpCoolTime());
        }
    }
    IEnumerator JumpCoolTime()  // Jump Cool Time
    {
        yield return new WaitForSeconds(jumpCooltime);
        isFlying = false;
    }


    // Start is called before the first frame update
    void Start()
    {
    }
    private void FixedUpdate()
    {
        Jumping();
        Movement();
    }
    void Update()
    {
        moveDirection = SetDirection();
        if (Input.GetKeyDown(KeyCode.Space) && !isFlying) isJumping = true;
    }
}
