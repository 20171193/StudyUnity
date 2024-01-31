using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TestShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject crossHair;

    public ParticleSystem shootingParticle;
    public Transform shootingTransform;

    [SerializeField]
    bool isShootingMode;    // 조준 모드

    private void Awake()
    {
        isShootingMode = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
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
        }

        if (isShootingMode && Input.GetButtonDown("Fire1"))
        {
            var temp = Instantiate(bulletPrefab, shootingTransform.position, shootingTransform.rotation);
            shootingParticle.Play();
            temp.GetComponent<TestBullet>().crossHair = crossHair;
        }
    }
}
