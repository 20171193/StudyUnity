using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManagement : MonoBehaviour
{
    [SerializeField]
    Animator titleAnimator;

    [Header("���̵��� �̹���")]
    [SerializeField]
    GameObject fadeImage;

    public void FadeTitle()
    {
        //fadeImage.SetActive(true);
        titleAnimator.Play("StartAnimation");
        StartCoroutine(FadeCo());
    }

    public void OnClickStartButton()
    {
        FadeTitle();
    }

    IEnumerator FadeCo()
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("TestGameScene");
    }
}
