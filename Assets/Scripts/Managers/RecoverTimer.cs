using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RecoverTimer : MonoBehaviour
{
    public static double RecoverSec;
    static Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        RecoverSec = 4;
    }

    // Update is called once per frame
    public async void RecoverCount()
    {
        while (RecoverSec >= 0.1)
        {
            RecoverSec -= 0.1;
            text.text = (Math.Floor(RecoverSec * 10) / 10).ToString();
            await Task.Delay(100);
        }
        RecoverSec = 4;
        text.text = "4.0";
    }
}
