using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static TestSpawnManager;

public enum SpawnType
{
    Enemy = 0
}

public class TestSpawnManager : MonoBehaviour
{
    #region �̱��� �޼���
    private static TestSpawnManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public static TestSpawnManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            else
                return instance;
        }
    }
    #endregion

    [SerializeField]
    private TestSpawner[] enemySpawners;

    [SerializeField]
    private TestSpawner[] patSpawners;


    private TestSpawner GetEnableSpawner(SpawnType spawnType)
    {
        switch(spawnType)
        {
            case SpawnType.Enemy:
                for(int i =0; i<enemySpawners.Length; i++)
                {
                    if (enemySpawners[i].GetIsSpawnable()) 
                        return enemySpawners[i];
                }
                return null;
            default:
                return null;
        }
    }


    // �뷮�� ���� spawner���� spawn.
    public void Spawn(SpawnType spawnType, int objectNumber)
    {
        TestSpawner targetSpawner = GetEnableSpawner(spawnType);
        if (targetSpawner == null)
            return;

        targetSpawner.Spawn(objectNumber);
    }
}
