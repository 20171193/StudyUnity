using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum FadeType
{
    IN = 0,
    OUT = 1
}

public class FadeManager : MonoBehaviour
{
    #region 싱글턴 메서드
    private static FadeManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        fadeImage = fadeObject.GetComponent<Image>();
        OnEndFade += EndFadeCoroutine;
    }

    public static FadeManager Instance
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

    Action OnEndFade;

    [Header("페이드 인/아웃 오브젝트")]
    [SerializeField]
    GameObject fadeObject;
    Image fadeImage;

    [Header("페이드 인/아웃 속도")]
    [SerializeField]
    float fadeSpeed; 

    public void Fade(FadeType fadeType)
    {
        Time.timeScale = 0f;
        switch(fadeType)
        {
            case FadeType.IN:
                fadeObject.SetActive(true);
                break;
            case FadeType.OUT:
                fadeObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    IEnumerator FadeIn(Action OnEndFade)
    {
        while(fadeImage.color.a < 1)
        {
            fadeImage.color = new Color(0, 0, 0, fadeImage.color.a + Time.deltaTime * fadeSpeed);
            yield return null;
        }
        OnEndFade?.Invoke();
    }
    IEnumerator FadeOut(Action OnEndFade)
    {
        while (fadeImage.color.a > 0)
        {
            fadeImage.color = new Color(0, 0, 0, fadeImage.color.a - Time.deltaTime * fadeSpeed);
            yield return null;
        }
        OnEndFade?.Invoke();
    }

    void EndFadeCoroutine()
    {
        Time.timeScale = 1f;
        fadeObject.SetActive(false);
    }
}
