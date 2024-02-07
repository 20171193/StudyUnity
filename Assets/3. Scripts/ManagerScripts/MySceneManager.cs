using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LoadType
{
    Normal = 0,
    Fade
}

public class MySceneManager : MonoBehaviour
{
    [Header("�ε��� ���� �ε� Ÿ��")]
    [SerializeField]
    LoadType curSceneLoadType;
    public LoadType CurSceneLoadType { get { return curSceneLoadType; } }

    public void SceneLoad(string name, LoadType loadType)
    {
        SceneManager.LoadScene(name);
    }
    public void SceneLoad(int index, LoadType loadType)
    {
        SceneManager.LoadScene(index);
    }
}
