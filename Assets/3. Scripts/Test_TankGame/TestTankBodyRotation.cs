using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TestTankBodyRotation : MonoBehaviour
{
    [SerializeField]    
    Transform bodyTransform;

    [SerializeField]
    float bodyRotSpeed = 300f;   // 몸체 회전속도

    private void Awake()
    {
        bodyTransform = gameObject.GetComponent<Transform>();
    }

    public void Rotatement()
    {
        bodyTransform.transform.Rotate(SetDirection());
    }

    private Vector3 SetDirection()
    {
        float inputHzt = Input.GetAxis("Horizontal");
        return new Vector3(0.0f, bodyRotSpeed * inputHzt * Time.deltaTime, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Rotatement();
    }
    private void FixedUpdate()
    {
        
    }
}
