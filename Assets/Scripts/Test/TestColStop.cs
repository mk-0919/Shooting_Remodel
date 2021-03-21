using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestColStop : MonoBehaviour
{
    public Text Text;
    bool Click = false;
    float TextNum = 0;
    IEnumerator cor;

    public void ButtonClick()
    {
        if(Click == false)
        {
            StartCoroutine("Count");
            Click = true;
        }
        else
        {
            StopCoroutine("Count");
            Click = false;
        }
    }

    private IEnumerator Count()
    {
        while (true)
        {
            TextNum += 0.1f;
            Text.text = ""+TextNum;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void ButtonClick2()
    {
        if (Click) StopCoroutine(cor);
        cor = null;
        cor = Count();
        StartCoroutine(cor);
        Click = true;
    }
    private IEnumerator Count2(float num)
    {
        while (true)
        {
            TextNum += 0.1f;
            Text.text = "" + TextNum;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
