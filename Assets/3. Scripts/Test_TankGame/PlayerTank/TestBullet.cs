using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject crossHair;
    public GameObject particle;

    public AudioSource audioSource;
    [HideInInspector]
    public float projectileSpeed;
    public float damage = 20f;

    Coroutine curCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        rb = GetComponent<Rigidbody>();
        Vector3 direction = crossHair.transform.position - this.transform.position;
        rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
        //rb.AddForce(Vector3.down * 400f, ForceMode.Force);  // 포탄 중력적용
        curCoroutine = StartCoroutine(DestroyBullet());
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (curCoroutine != null) 
            StopCoroutine(curCoroutine);
        
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
