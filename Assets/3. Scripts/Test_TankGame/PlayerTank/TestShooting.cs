using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class TestShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject crossHair;
    public GameObject zoomCanvas;

    public GameObject crossHairBack;
    public Image crossHairBackImage;

    public Transform zoomOutCameraPos;

    public ParticleSystem shootingParticle;
    public Transform shootingTransform;

    [SerializeField]
    bool isShootingMode;    // 조준 모드

    float bulletPower = 2.0f;      // 총알 속도
    float addBulletForce = 0.1f;   // 
    Coroutine pressedCo;

    private void Awake()
    {
        isShootingMode = false;
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
    }
    private void OnShooting(InputValue value)
    {
        if (!isShootingMode) return;

        if(value.isPressed)
        {
            pressedCo = StartCoroutine(PressdMouseButton());
        }
        else
        {
            if (pressedCo != null)
                StopCoroutine(pressedCo);
            Fire(bulletPower);
            bulletPower = 2.0f;
            crossHairBack.transform.localRotation = Quaternion.identity;
            crossHairBackImage.color = new Color(1, 1, 1, 0.3f);
        }
    }

    IEnumerator PressdMouseButton()
    {
        while(true)
        {
            bulletPower += addBulletForce;
            crossHairBack.transform.Rotate(0, 0, crossHairBack.transform.rotation.z + 5, Space.Self);
            crossHairBackImage.color
                = new Color(crossHairBackImage.color.r,  crossHairBackImage.color.g, crossHairBackImage.color.b, crossHairBackImage.color.a + 0.02f);
            

            Debug.Log(crossHairBackImage.color);
            yield return new WaitForSeconds(0.1f);
        }
    }



    private void SettingShootingMode()
    {
        if (isShootingMode)
        {
            crossHair.gameObject.SetActive(false);
            isShootingMode = false;
        }
        else
        {
            crossHair.gameObject.SetActive(true);
            isShootingMode = true;
        }
        Zoom();
    }
    private void Zoom()
    {
        if (isShootingMode)
        {
            zoomCanvas.SetActive(true);
            Camera.main.transform.parent = shootingTransform;
            Camera.main.transform.position = shootingTransform.position + shootingTransform.forward * 0.5f;
            Camera.main.transform.rotation = shootingTransform.rotation;
        }
        else
        {
            zoomCanvas.SetActive(false);
            Camera.main.transform.parent = zoomOutCameraPos;
            Camera.main.transform.position = zoomOutCameraPos.position;
            Camera.main.transform.rotation = zoomOutCameraPos.rotation;
        }
    }
    private void OnZoom(InputValue value)
    {
        SettingShootingMode();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
