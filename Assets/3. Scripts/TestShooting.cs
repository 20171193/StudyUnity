using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TestShooting : MonoBehaviour
{
    public GameObject bullet;
    public ParticleSystem myParticle;
    public Transform shootingTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var temp = Instantiate(bullet, shootingTransform);
            myParticle.Play();
            temp.GetComponent<TestBullet>().turret = this.gameObject;
        }
    }
}
