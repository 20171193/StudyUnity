using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestMoveAgent : MonoBehaviour
{
    public Camera mainCamera;
    public Transform cameraTransform;
    public Rigidbody myRb;
    public BoxCollider myBcol;
    public float moveSpeed = 10f;
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

        if(collision.gameObject.layer == Floor_Layer)
        {
            isGround = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == Floor_Layer)
        {
            isGround = false;
        }
    }
    public void Jump()
    {
        if(isGround && Input.GetKeyDown(KeyCode.Space))
        {
            // 점프
            myRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(h, 0, v);

        //moveDirection = cameraTransform.TransformDirection(moveDirection);
        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Movement();
    }
}
