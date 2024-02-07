using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMoveTank : MonoBehaviour
{
    public Camera mainCamera;

    public Rigidbody myRb;
    public BoxCollider myBcol;

    public Transform tankBodyTransform;

    public float moveSpeed;       // 이동속도
    public float maxVelocity;

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
        moveDirection = Vector3.zero;
        moveSpeed = myRb.mass * 20f;
    }

    void Start()
    {
        StartCoroutine(PrintVelocity());
    }
    private void FixedUpdate()
    {
        Jumping();
        Movement();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFlying) isJumping = true;
    }

    private void OnMove(InputValue value) // Setting Move Direction
    {
        Vector2 getValue = value.Get<Vector2>();
        moveDirection.x = getValue.x;
        moveDirection.z = getValue.y;
    }
    private void Movement()     // Move
    {
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        myRb.AddForce(tankBodyTransform.forward * moveDirection.z * moveSpeed, ForceMode.Force);

        if (myRb.velocity.x > 10) 
            myRb.velocity = new Vector3(maxVelocity, myRb.velocity.y, myRb.velocity.z); 
        else if(myRb.velocity.x < -10)
            myRb.velocity = new Vector3(-maxVelocity, myRb.velocity.y, myRb.velocity.z);

        if (myRb.velocity.z > 10)
            myRb.velocity = new Vector3(myRb.velocity.x, myRb.velocity.y, maxVelocity);
        else if (myRb.velocity.z < -10)
            myRb.velocity = new Vector3(myRb.velocity.x, myRb.velocity.y, -maxVelocity);
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


    IEnumerator PrintVelocity()
    {
        yield return new WaitForSeconds(1.0f);
        //Debug.Log("Agent Speed : " + myRb.velocity);
        StartCoroutine(PrintVelocity());
    }

    // Start is called before the first frame update
    #region Collision Fucntion
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
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
}
