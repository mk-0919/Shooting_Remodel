using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Image Image;
    [SerializeField] private RenderTexture Rend;
    public AudioSource Click,OnButton;
    float fadeSpeed = 0.02f;
    float r, g, b, a;
    public Button first, wave, score;

    private void Awake()
    {
        r = Image.color.r;
        g = Image.color.g;
        b = Image.color.b;
        a = Image.color.a;
    }
    public void firstClick()
    {
        Click.Play();
        first.gameObject.SetActive(false);
        wave.gameObject.SetActive(true);
        score.gameObject.SetActive(true);
    }
    public void PointerEnter()
    {
        OnButton.Play();
    }
    public void WaveStart()
    {
        Click.Play();
        StartCoroutine(SceneChenge(3));
    }
    public void ScoreStart()
    {
        Click.Play();
        StartCoroutine(SceneChenge(1));
    }
    private IEnumerator SceneChenge(int num)
    {
        Image.enabled = true;
        ButtonHide();
        while (a < 1)
        {
            a += fadeSpeed;
            yield return null;
            Image.color = new Color(r, g, b, a);
        }
        yield return new WaitForSeconds(1);
        Rend.Release();
        SceneManager.LoadScene(num);
    }
    private void ButtonHide()
    {
        first.gameObject.SetActive(false);
        wave.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
    }
}
