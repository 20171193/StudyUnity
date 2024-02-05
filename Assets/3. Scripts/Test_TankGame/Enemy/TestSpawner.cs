using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField]
    protected int objectCapacity;
    [SerializeField]
    protected int objectCount;
    [SerializeField]
    protected GameObject[] spanwedArray;

    public bool IsSpawnable()
    {
        return objectCapacity > objectCount;
    }
    public void Spawn()
    {

    }
}
