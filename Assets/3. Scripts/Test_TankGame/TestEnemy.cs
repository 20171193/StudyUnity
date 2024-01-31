using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public Material hitMaterials;
    public Material baseMaterials;

    public GameObject[] setMaterialTargets;

    public GameObject particlePrefab;

    public float ownHp = 100f;

    public void TakeDamage(float damage, Collision col)
    {
        if(ownHp - damage <= 0)
        {
            Explosion(col); 
            return;
        }

        ownHp -= damage;
        for(int i=0; i<setMaterialTargets.Length; i++)
        {
            setMaterialTargets[i].GetComponent<MeshRenderer>().material = hitMaterials;
        }
        StartCoroutine(ResetMaterial());
    }
    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < setMaterialTargets.Length; i++)
        {
            setMaterialTargets[i].GetComponent<MeshRenderer>().material = baseMaterials;
        }
    }
    public void Explosion(Collision col)
    {
        ContactPoint cPoint = col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, cPoint.normal);
        var eParticle = Instantiate(particlePrefab, cPoint.point, rot);
        eParticle.GetComponent<ParticleSystem>().Play();
        Destroy(eParticle, 2);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌체크");
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("총알확인");
            TakeDamage(collision.gameObject.GetComponent<TestBullet>().damage, collision);
        }
    }
}
