using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestSpawner : MonoBehaviour
{
    [SerializeField]
    int objectCapacity;
    [SerializeField]
    int objectCount;

    [SerializeField]
    float respawnTime;


    [SerializeField]
    GameObject[] spawnedArray;

    [Header("스폰할 오프젝트")]
    [SerializeField]
    GameObject[] spawnPrefabs;

    private void Awake()
    {
        spawnedArray = new GameObject[objectCapacity];
        objectCount = 0;
    }
    private void Start()
    {
        Spawn(-1);
    }

    public bool GetIsSpawnable()
    {
        return objectCapacity > objectCount;
    }
    private int FindSpawnableIndex()
    {
        for(int i=0; i< spawnedArray.Length; i++)
        {
            if (spawnedArray[i] == null) return i;
        }

        return -1;
    }

    public void DeathObject(int objectNumber)
    {
        StartCoroutine(SpawnDelay(objectNumber));
    }
    IEnumerator SpawnDelay(int objectNumber)
    {
        yield return new WaitForSeconds(2.0f);
        Spawn(objectNumber);
    }

    public void Spawn(int objectNumber)
    {
        // 랜덤한 오브젝트 스폰
        if(objectNumber == -1)
            objectNumber = Random.Range(0, spawnPrefabs.Length - 1);
        else if(objectNumber >= spawnPrefabs.Length)
        {
            Debug.Log($"{objectNumber}의 오브젝트가 존재하지 않습니다.");
            return;
        }
        int index = FindSpawnableIndex();
        spawnedArray[index] = Instantiate(spawnPrefabs[objectNumber], transform.position, transform.rotation);
        spawnedArray[index].GetComponent<TestEnemy>().mySpawner = this;
        objectCount++;
    }
}
