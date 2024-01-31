using UnityEngine;

public class TestMoveAgent : MonoBehaviour
{
    public Camera mainCamera;

    public Transform bodyTransform;
    public Transform turretTransform;

    public Rigidbody myRb;
    public BoxCollider myBcol;

    public float moveSpeed = 10f;
    public float bodyRotSpeed = 120f;
    public float turretRotSpeed = 200f;

    public float jumpForce = 5f;

    public bool isGround = false;

    const int Floor_Layer = 7;

    public TestMoveAgent()
    {
        try
        {
            myRb = this.gameObject.GetComponent<Rigidbody>();
            myBcol = this.gameObject.GetComponent<BoxCollider>();
        }
        catch
        {
            Debug.Log("에이전트 생성자 세팅 오류");
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌");

        if (collision.gameObject.layer == Floor_Layer)
        {
            isGround = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == Floor_Layer)
        {
            isGround = false;
        }
    }
    public void Jump()
    {
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            // 점프
            myRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void TurretRotatement()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        
        Vector3 dir = new Vector3(0, h * 10f, 0);
        Vector3 angle = turretTransform.transform.eulerAngles;

        angle += turretRotSpeed * dir * Time.deltaTime;
        turretTransform.eulerAngles = angle;
    }

    public void Rotatement()
    {
        float h = Input.GetAxis("Horizontal");
        bodyTransform.transform.Rotate(new Vector3(0.0f, bodyRotSpeed * h * Time.deltaTime, 0.0f));
    }
    public void Movement()
    {
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = bodyTransform.transform.forward;

        this.transform.Translate(v * moveDirection * moveSpeed * Time.deltaTime);
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void FixedUpdate()
    {

    }
    void Update()
    {
        Jump();
        TurretRotatement();
        Rotatement();
        Movement();
    }
}
