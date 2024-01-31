using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject turret;
    public GameObject particle;
    public AudioSource audioSource;
    public float moveSpeed = 10f;
    public float damage = 20f;

    Coroutine curCoroutine;
    public TestBullet()
    {
        //rb.AddForce()
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        rb = GetComponent<Rigidbody>();
        Vector3 direction = transform.position - turret.transform.position;
        rb.AddForce(new Vector3(direction.x, 0,direction.z) * moveSpeed, ForceMode.Impulse);
        curCoroutine = StartCoroutine(DestroyBullet());
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(curCoroutine != null) StopCoroutine(curCoroutine);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
