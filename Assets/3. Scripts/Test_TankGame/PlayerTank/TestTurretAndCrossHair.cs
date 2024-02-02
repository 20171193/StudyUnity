using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestTurretAndCrossHair : MonoBehaviour
{
    public Transform turretTransform;

    [SerializeField]
    float turretRotHztSpeed = 15f; // �ͷ� �¿� ȸ���ӵ�
    [SerializeField]
    float turretRotVtcSpeed = 10f;  // �ͷ� ���� ȸ���ӵ�
    [SerializeField]
    float mouseSensitivity = 10f;   // ���콺 �ΰ���

    [SerializeField]    
    Vector3 angle;      // result angle

    private float xRotation;
    private float yRotation;

    private void Awake()
    {
        angle = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        TurretRotatement();
    }

    private void FixedUpdate()
    {

    }

    public void SetAngle()
    {
        //float inputHzt = Input.GetAxis("Mouse X");
        //float inputVtc = Input.GetAxis("Mouse Y");

        //Vector3 direction = new Vector3(
        //    -inputVtc * mouseSensitivity * turretRotVtcSpeed,
        //    inputHzt * mouseSensitivity * turretRotHztSpeed,
        //    0f);
        //angle = turretTransform.transform.eulerAngles;
        //angle += direction * Time.deltaTime;

        //// ��/�� ȸ�� ����
        //angle.x = Mathf.Clamp(angle.x, 10, 330);

        yRotation += Input.GetAxis("Mouse X"); //turretRotHztSpeed;
        xRotation -= Input.GetAxis("Mouse Y"); //turretRotVtcSpeed;
        xRotation = Mathf.Clamp(xRotation, -80f, 10f);
    }

    public void TurretRotatement()
    {
        SetAngle();
        turretTransform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

}
