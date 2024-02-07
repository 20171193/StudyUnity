using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum FadeType
{
    IN = 0,
    OUT,
    INOUT
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
        
        Fade(FadeType.OUT);
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

    [Header("페이드 인/아웃 오브젝트")]
    [SerializeField]
    GameObject fadeObject;
    [SerializeField]
    Image fadeImage;

    [Header("페이드 인/아웃 속도")]
    [SerializeField]
    float fadeSpeed;

    Action OnEndFade;   // 페이드 코루틴 종료 후 초기화 액션

    public void Fade(FadeType fadeType)
    {
        //Time.timeScale = 0f;
        switch(fadeType)
        {
            case FadeType.IN:
                InitFadeSetting(fadeType);
                StartCoroutine(FadeIn(OnEndFade));
                break;
            case FadeType.OUT:
                InitFadeSetting(fadeType);
                StartCoroutine(FadeOut(OnEndFade));
                break;
            case FadeType.INOUT:
                InitFadeSetting(fadeType);
                StartCoroutine(FadeInOut(OnEndFade));
                break;
            default:
                break;
        }
    }
    void InitFadeSetting(FadeType fadeType)
    {
        fadeObject.SetActive(true);
        switch (fadeType)
        {
            case FadeType.IN:
                fadeImage.color = new Color(0, 0, 0, 0);
                break;
            case FadeType.OUT:
                fadeImage.color = new Color(0, 0, 0, 1);
                break;
            case FadeType.INOUT:
                fadeImage.color = new Color(0, 0, 0, 0.01f);
                break;
            default:
                break;
        }
    }
    void EndFadeCoroutine()
    {
        Time.timeScale = 1f;
        fadeObject.SetActive(false);
    }
    IEnumerator FadeIn(Action OnEndFadeIn)
    {
        while(fadeImage.color.a < 1)
        {
            fadeImage.color = new Color(0, 0, 0, fadeImage.color.a + Time.deltaTime * fadeSpeed);
            Debug.Log(Time.deltaTime * fadeSpeed);
            yield return null;
        }
        OnEndFadeIn?.Invoke();
    }
    IEnumerator FadeOut(Action OnEndFadeOut)
    {
        while (fadeImage.color.a > 0)
        {
            fadeImage.color = new Color(0, 0, 0, fadeImage.color.a - Time.deltaTime * fadeSpeed);
            yield return null;
        }
        OnEndFadeOut?.Invoke();
    }
    IEnumerator FadeInOut(Action OnEndFadeInOut)
    {
        bool isIncrease = true;
        while (fadeImage.color.a > 0)
        {
            if (isIncrease && fadeImage.color.a >= 1) 
                isIncrease = false;

            if(isIncrease)
                fadeImage.color = new Color(0, 0, 0, fadeImage.color.a + Time.deltaTime * fadeSpeed);
            else
                fadeImage.color = new Color(0, 0, 0, fadeImage.color.a - Time.deltaTime * fadeSpeed);
            yield return null;
        }
        OnEndFadeInOut?.Invoke();
    }
}
