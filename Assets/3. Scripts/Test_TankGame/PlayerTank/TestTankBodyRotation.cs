using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestTankBodyRotation : MonoBehaviour
{
    [SerializeField]    
    Transform bodyTransform;

    [SerializeField]
    public float bodyRotSpeed;   // 몸체 회전속도

    Vector3 rotDirection;

    private void Awake()
    {
        rotDirection = Vector3.zero;
    }

    public void Rotatement()
    {
        bodyTransform.transform.Rotate(rotDirection);
    }

    private void OnRotate(InputValue value)
    {
        Vector2 getValue = value.Get<Vector2>();
        Debug.Log(getValue);
        //float inputHzt = Input.GetAxis("Horizontal");
        rotDirection = new Vector3(0.0f,  getValue.x * bodyRotSpeed * Time.deltaTime, 0.0f);
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
