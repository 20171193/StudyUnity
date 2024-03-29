using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public enum CameraZoomType
{
    // Index of cvCameras
    LongZoom = 0,
    ShortZoom = 1,
    NormalZoom = 2,

    CurZoom = 10    // ���� ����
}


public class TestShooting : MonoBehaviour
{
    [Header("INDEX [ Long:0 | Short:1 | Normal:2 ]")]
    public CinemachineVirtualCamera[] cvCameras;
    public int curZoomType; // ���� �� Ÿ��

    [Header("�Ѿ� ������")]
    public GameObject bulletPrefab;
    [Header("ũ�ν���� ĵ����")]
    public GameObject crossHair;
    [Header("�� ĵ����(���� ���� �̹���)")]
    public GameObject zoomCanvas;

    [Header("ũ�ν���� �޹�� GO")]
    public GameObject crossHairBack;
    [Header("ũ�ν���� �޹�� �̹���")]
    public Image crossHairBackImage;

    public Transform zoomOutCameraPos;

    public ParticleSystem shootingParticle;
    public Transform shootingTransform;

    private Animator tankAnim;

    [SerializeField]
    bool isShootingMode;    // ���� ���

    float bulletPower = 2.0f;      // �⺻ ���� �ӵ�
    float addBulletForce = 0.5f;   // ���� �ӵ� ������

    float zoomInTime = 0.2f;    // ���� ���ؽð� (0.5�ʰ� Ŭ���� �����ϸ� ����, �ƴ϶�� ����)

    [SerializeField]
    bool isLongZoom = false;    // ����
    [SerializeField]
    bool isShortZoom = false;   // ����

    Coroutine bulletPowerCo;    // ���� �Ŀ� �ڷ�ƾ
    Coroutine zoomTimerCo;   // ���� �ð� �ڷ�ƾ

    private void Awake()
    {
        isShootingMode = false;
        crossHair.SetActive(false);
        zoomCanvas.SetActive(false);
        tankAnim = gameObject.GetComponent<Animator>();
        ZoomOutSetting();
    }
    private void Fire(float power)
    {
        var temp = Instantiate(bulletPrefab, shootingTransform.position, shootingTransform.rotation);
        shootingParticle.Play();
        temp.GetComponent<TestBullet>().crossHair = crossHair;
        temp.GetComponent<TestBullet>().projectileSpeed = bulletPower;
        tankAnim.SetTrigger("Shoot");
    }

    // mouse button callback
    private void OnZoom(InputValue value)
    {
        // ������ �ִ� ��� : ����
        // ª�� ������ �� ��� : ����

        // ���� ���
        if (value.isPressed)
        {
            // ���� �����ִ� ��� (�ܾƿ�)
            if (isShortZoom || isLongZoom)
            {
                ZoomOut();
            }
            // ���� �������� ���� ��� (����)
            else
            {
                zoomTimerCo = StartCoroutine(ZoomInTimer());
                isLongZoom = true;
            }
        }
        // �� ���
        else
        {
            // ������ �������� ��
            if (isShortZoom)
            {
                // �ܾƿ�
                ZoomOut();
            }
            else if (isLongZoom)
            {
                // �ڷ�ƾ ���� �� ������
                StopCoroutine(zoomTimerCo);
                ZoomIn(CameraZoomType.LongZoom);
            }
        }
    }
    private void OnShooting(InputValue value)
    {
        if (!isShortZoom && !isLongZoom) return;

        if (value.isPressed)
        {
            bulletPowerCo = StartCoroutine(PressedShooting());
        }
        else
        {
            if (bulletPowerCo != null)
                StopCoroutine(bulletPowerCo);
            Fire(bulletPower);
            bulletPower = 2.0f;
            crossHairBack.transform.localRotation = Quaternion.identity;
            crossHairBackImage.color = new Color(1, 1, 1, 0.3f);
        }
    }

    IEnumerator PressedShooting()
    {
        // ���� �ӵ� ����
        while (true)
        {
            bulletPower += addBulletForce;
            crossHairBack.transform.Rotate(0, 0, crossHairBack.transform.rotation.z + 5, Space.Self);
            crossHairBackImage.color
                = new Color(crossHairBackImage.color.r, crossHairBackImage.color.g, crossHairBackImage.color.b, crossHairBackImage.color.a + 0.02f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator ZoomInTimer()
    {
        yield return new WaitForSeconds(zoomInTime);
        ZoomIn(CameraZoomType.ShortZoom);
        isShortZoom = true;
        isLongZoom = false;
    }
    private void ZoomIn(CameraZoomType zoomType)
    {
        ZoomInSetting(zoomType);
        crossHair.SetActive(true);
        if (zoomType == CameraZoomType.LongZoom)
            zoomCanvas.SetActive(true);
    }
    private void ZoomOut()
    {
        if (zoomCanvas.activeSelf)
            zoomCanvas.SetActive(false);
        if (crossHair.activeSelf)
            crossHair.SetActive(false);

        isShortZoom = false;
        isLongZoom = false;
        ZoomOutSetting();
    }

    public void ZoomInSetting(CameraZoomType zoomType)
    {
        ZoomOutSetting();
        curZoomType = (int)zoomType;
        switch (zoomType)
        {
            case CameraZoomType.LongZoom:
                cvCameras[(int)CameraZoomType.LongZoom].Priority = (int)CameraZoomType.CurZoom;
                break;
            case CameraZoomType.ShortZoom:
                cvCameras[(int)CameraZoomType.ShortZoom].Priority = (int)CameraZoomType.CurZoom;
                break;
            default:
                curZoomType = (int)CameraZoomType.NormalZoom;
                break;
        }
    }
    public void ZoomOutSetting()
    {
        Debug.Log("zoom init");
        curZoomType = (int)CameraZoomType.NormalZoom;
        cvCameras[(int)CameraZoomType.LongZoom].Priority = (int)CameraZoomType.LongZoom;
        cvCameras[(int)CameraZoomType.ShortZoom].Priority = (int)CameraZoomType.ShortZoom;
        cvCameras[(int)CameraZoomType.NormalZoom].Priority = (int)CameraZoomType.NormalZoom;
    }


}
