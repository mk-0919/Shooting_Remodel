using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartProcess : MonoBehaviour
{
    public static bool processPermit;
    public Text text;
    public Image Image;
    public AudioSource Audio;
    float fadeSpeed = 0.02f;
    float r, g, b, a;

    private void Awake()
    {
        Image.enabled = true;
        r = Image.color.r;
        g = Image.color.g;
        b = Image.color.b;
        a = Image.color.a;
        processPermit = false;
    }
    private void Start()
    {
        StartCoroutine("SceneChenge");
    }
    private IEnumerator SceneChenge()
    {
        yield return new WaitForSeconds(1);
        while (a > 0)
        {
            a -= fadeSpeed;
            yield return null;
            Image.color = new Color(r, g, b, a);
            Audio.volume += (fadeSpeed / 10);
        }
        Image.enabled = false;
        StartCoroutine("StartCount");
    }
    private IEnumerator StartCount()
    {
        text.enabled = true;
        int Count = 3;
        while(Count > 0)
        {
            text.text = Count.ToString();
            Count--;
            yield return new WaitForSeconds(1);
        }
        text.text = "Start";
        processPermit = true;
        yield return new WaitForSeconds(1);
        text.enabled = false;
    }
}