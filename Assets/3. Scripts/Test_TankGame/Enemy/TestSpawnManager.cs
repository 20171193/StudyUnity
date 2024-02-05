using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TestSpawnManager;

public enum SpawnType
{
    Enemy = 0
}

public class TestSpawnManager : MonoBehaviour
{
    #region 싱글턴 메서드
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


    public void Spawn(TestSpawner spawner, SpawnType spawnType)
    {
        switch(spawnType)
        {
            
        }
    }
}
