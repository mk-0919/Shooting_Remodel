using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ReloadTimer : MonoBehaviour
{
    public static double reloadSec;
    static Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        reloadSec = 3;
    }

    // Update is called once per frame
    public async void ReloadCount()
    {
        while (reloadSec >= 0.1)
        {
            reloadSec -= 0.1;
            text.text = (Math.Floor(reloadSec * 10) / 10).ToString();
            await Task.Delay(100);
        }
        reloadSec = 3;
        text.text = "3.0";
    }
}
