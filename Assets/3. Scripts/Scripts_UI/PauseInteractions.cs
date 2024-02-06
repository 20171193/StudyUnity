using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInteractions : MonoBehaviour
{
    [Header("게임 재개 대기시간")]  
    [SerializeField]
    float continueCoolTime;
    float curTime = 0f;
    public float CurTime { get { return curTime; } }

    public void OnClickContinue()
    {
        StartCoroutine(ContinueTimer());
    }
    public void OnClickOption()
    {
        // Open Option PopUp
    }
    public void OnClickExit()
    {
        // Game Exit
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); 
        #endif
    }

    IEnumerator ContinueTimer()
    {
        curTime = continueCoolTime;
        while(curTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            curTime -= 1.0f;
        }
    }
}
