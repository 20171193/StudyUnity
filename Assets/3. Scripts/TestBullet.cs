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
        rb.AddForce(new Vector3(direction.x, 0f,direction.z) * moveSpeed, ForceMode.Impulse);
        ShootingParticle();
    }
    IEnumerator ShootingParticle()
    {
        yield return new WaitForSeconds(2.0f);
        Debug.Log("지우는 상태");
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
