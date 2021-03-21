using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveNumManager : MonoBehaviour
{
    public Text text, high;
    public static int[] WaveRanks;
    public Text[] texts;
    private static bool WaveResetflag = true;
    public static int WaveNum;
    public static int Wsave, Wsave1, Wsave2, Whigh;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            Wsave = 1;
            WaveNum = 1;
        }
        if (WaveResetflag)
        {
            WaveRanks = new int[11];
            for (int i = 0; i < 11; i++)
            {
                WaveRanks[i] = 0;
            }
            WaveResetflag = false;
        }
        Whigh = PlayerPrefs.GetInt("HighWave", 0);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            text.text = "Wave:" + WaveNum;
            Wsave = WaveNum;
            if(Whigh <= WaveNum)
            {
                Whigh = WaveNum;
                PlayerPrefs.SetInt("HighWave", Whigh);
                PlayerPrefs.Save();
            }
            PlayerPrefs.SetInt("WaveRank" + 10, WaveNum);
            PlayerPrefs.Save();
        }
        else
        {
            NumSort();
            WaveResultUpdate();
        }
        Debug.Log(Whigh);
    }
    void NumSort()
    {
        bool isEnd = false;
        int finAdjust = 1;
        while (!isEnd)
        {
            bool loopSwap = false;
            for (int i=0;i<WaveRanks.Length - finAdjust; i++)
            {
                if (PlayerPrefs.GetInt("WaveRank" + i,0) < PlayerPrefs.GetInt("WaveRank" + (i + 1), 0))
                {
                    Wsave1 = PlayerPrefs.GetInt("WaveRank" + i, 0);
                    Wsave2 = PlayerPrefs.GetInt("WaveRank" + (i + 1), 0);
                    PlayerPrefs.SetInt("WaveRank" + i, Wsave2);
                    PlayerPrefs.SetInt("WaveRank" + (i + 1), Wsave1);
                    PlayerPrefs.Save();
                    loopSwap = true;
                }
            }
            if (!loopSwap)
            {
                isEnd = true;
            }
            finAdjust++;
        }
    }
    void WaveResultUpdate()
    {
        for (int i = 1; i < 11; i++)
        {
            texts[i - 1].text = i + " :" + PlayerPrefs.GetInt("WaveRank" + (i - 1), 0);
        }
        text.text = "Wave:" + Wsave;
        high.text = "HighWave:" + PlayerPrefs.GetInt("HighWave", 0);
    }
}
