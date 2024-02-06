using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class TestShooting : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject bulletPrefab;
    [Header("크로스헤어 캔버스")]
    public GameObject crossHair;
    [Header("줌 캔버스(줌인 전용 이미지)")]
    public GameObject zoomCanvas;

    [Header("크로스헤어 뒷배경 GO")]
    public GameObject crossHairBack;
    [Header("크로스헤어 뒷배경 이미지")]
    public Image crossHairBackImage;

    public Transform zoomOutCameraPos;

    public ParticleSystem shootingParticle;
    public Transform shootingTransform;

    private Animator tankAnim;

    [SerializeField]
    bool isShootingMode;    // 조준 모드

    float bulletPower = 2.0f;      // 기본 대포 속도
    float addBulletForce = 0.5f;   // 대포 속도 증가량

    float zoomInTime = 0.2f;    // 줌인 기준시간 (0.5초간 클릭을 유지하면 롱줌, 아니라면 숏줌)

    [SerializeField]
    bool isLongZoom = false;    // 롱줌
    [SerializeField]
    bool isShortZoom = false;   // 숏줌

    Coroutine bulletPowerCo;    // 슈팅 파워 코루틴
    Coroutine zoomTimerCo;   // 줌인 시간 코루틴

    private void Awake()
    {
        isShootingMode = false;
        crossHair.SetActive(false);
        zoomCanvas.SetActive(false);
        tankAnim = gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    private void Fire(float power)
    {
        var temp = Instantiate(bulletPrefab, shootingTransform.position, shootingTransform.rotation);
        shootingParticle.Play();
        temp.GetComponent<TestBullet>().crossHair = crossHair;
        temp.GetComponent<TestBullet>().projectileSpeed = bulletPower;
        tankAnim.SetTrigger("Shoot");
    }
    private void Rebound()
    {

    }
    private void OnShooting(InputValue value)
    {
        if (!isShortZoom && !isLongZoom) return;

        if(value.isPressed)
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
    IEnumerator PressedShooting( )
    {
        // 대포 속도 증가
        while(true)
        {
            bulletPower += addBulletForce;
            crossHairBack.transform.Rotate(0, 0, crossHairBack.transform.rotation.z + 5, Space.Self);
            crossHairBackImage.color
                = new Color(crossHairBackImage.color.r,  crossHairBackImage.color.g, crossHairBackImage.color.b, crossHairBackImage.color.a + 0.02f);
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
        TestCameraManager.Instance.ZoomIn(zoomType);
        crossHair.SetActive(true);
        if (zoomType == CameraZoomType.LongZoom)
            zoomCanvas.SetActive(true);
    }
    private void ZoomOut()
    {
        if(zoomCanvas.activeSelf)
            zoomCanvas.SetActive(false);
        if(crossHair.activeSelf)
            crossHair.SetActive(false);
        
        isShortZoom = false;
        isLongZoom = false;
        TestCameraManager.Instance.ZoomOut();
    }
    private void OnZoom(InputValue value)
    {
        // 누르고 있는 경우 : 숏줌
        // 짧게 눌렀다 뗀 경우 : 롱줌

        // 눌린 경우
        if (value.isPressed)
        {
            // 줌이 켜져있는 경우 (줌아웃)
            if (isShortZoom || isLongZoom)
            {
                ZoomOut();
            }
            // 줌이 켜져있지 않은 경우 (줌인)
            else
            {
                zoomTimerCo = StartCoroutine(ZoomInTimer());
                isLongZoom = true;
            }
        }
        // 뗀 경우
        else
        {
            // 숏줌이 켜져있을 때
            if (isShortZoom)
            {
                // 줌아웃
                ZoomOut();
            }
            else if(isLongZoom)
            {
                // 코루틴 종료 후 롱줌인
                StopCoroutine(zoomTimerCo);
                ZoomIn(CameraZoomType.LongZoom);
            }
        }
    }
}
