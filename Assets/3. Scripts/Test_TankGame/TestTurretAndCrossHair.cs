using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestTurretAndCrossHair : MonoBehaviour
{

    private Transform turretTransform;

    [SerializeField]
    float turretRotHztSpeed = 200f; // 터렛 좌우 회전속도
    [SerializeField]
    float turretRotVtcSpeed = 50f;  // 터렛 상하 회전속도
    [SerializeField]
    float mouseSensitivity = 10f;   // 마우스 민감도

    [SerializeField]    
    Vector3 angle;      // result angle

    private void Awake()
    {
        turretTransform = gameObject.GetComponent<Transform>();
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
        float inputHzt = Input.GetAxis("Mouse X");
        float inputVtc = Input.GetAxis("Mouse Y");

        Vector3 direction = new Vector3(
            -inputVtc * mouseSensitivity * turretRotVtcSpeed,
            inputHzt * mouseSensitivity * turretRotHztSpeed,
            0f);
        angle = turretTransform.transform.eulerAngles;
        angle += direction * Time.deltaTime;

        // 상/하 회전 제한
        //if (angle.x >= 10 && angle.x < 300) angle.x = 10;
        //else if (angle.x >= 330 && angle.x <= 360) angle.x = 330;
    }

    public void TurretRotatement()
    {
        SetAngle();
        turretTransform.transform.eulerAngles = angle;
    }

}
