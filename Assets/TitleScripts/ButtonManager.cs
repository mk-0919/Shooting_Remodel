using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Image Image;
    [SerializeField] private RenderTexture Rend;
    AudioSource Click;
    float fadeSpeed = 0.02f;
    float r, g, b, a;

    private void Awake()
    {
        r = Image.color.r;
        g = Image.color.g;
        b = Image.color.b;
        a = Image.color.a;
        Click = GetComponent<AudioSource>();
    }
    public void GameStart()
    {
        Click.Play();
        StartCoroutine("SceneChenge");
    }
    private IEnumerator SceneChenge()
    {
        Image.enabled = true;
        while (a < 1)
        {
            a += fadeSpeed;
            yield return null;
            Image.color = new Color(r, g, b, a);
        }
        yield return new WaitForSeconds(1);
        Rend.Release();
        SceneManager.LoadScene(0);
    }
}
