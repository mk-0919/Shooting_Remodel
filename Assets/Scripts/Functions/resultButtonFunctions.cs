using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class resultButtonFunctions : MonoBehaviour
{
    public Image Image;
    public AudioSource OnButton, Click;
    float fadeSpeed = 0.02f;
    float r, g, b, a;
    private void Awake()
    {
        r = Image.color.r;
        g = Image.color.g;
        b = Image.color.b;
        a = Image.color.a;
    }
    public void ClickContinue()
    {
        Click.Play();
        StartCoroutine(SceneChenge(0));
    }
    public void ClickReturn()
    {
        Click.Play();
        StartCoroutine(SceneChenge(1));
    }
    public void PointerEnter()
    {
        OnButton.Play();
    }
    private IEnumerator SceneChenge(int num)
    {
        Image.enabled = true;
        while (a < 1)
        {
            a += fadeSpeed;
            yield return null;
            Image.color = new Color(r, g, b, a);
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(num);
    }
}
