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
    #region 싱글턴 메서드
    private static MySceneManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //this.sceneInfo = instance.sceneInfo;
            Destroy(this.gameObject);
        }
    }

    public static MySceneManager Instance
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

    [Header("로드할 씬의 로드 타입")]
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
