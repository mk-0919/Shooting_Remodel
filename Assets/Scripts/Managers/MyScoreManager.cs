using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyScoreManager : MonoBehaviour
{
    public Text text,high;
    public static int[] ranks;
    public Text[] texts;
    private static bool resetflag = true;
    public static int score;
    public static int save, save1, save2, highscore;
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            save = 0;
        }
        if (resetflag)
        {
            ranks = new int[11];
            for (int i = 0; i < 11; i++)
            {
                ranks[i] = 0;
            }
            resetflag = false;
        }
        highscore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            text.text = "Score: " + score;
            save = score;
            if(highscore <= score)
            {
                highscore = score;
                PlayerPrefs.SetInt("HighScore", highscore);
                PlayerPrefs.Save();
            }
            PlayerPrefs.SetInt("rank" + 10, score);
            PlayerPrefs.Save();
        }
        else
        {
            ScoreSort();
            resultUpdate();
        }
    }
    void ScoreSort()
    {
        bool isEnd = false;
        int finAdjust = 1;
        while (!isEnd)
        {
            bool loopSwap = false;
            for (int i = 0; i < ranks.Length - finAdjust; i++)
            {
                if (PlayerPrefs.GetInt("rank" + i,0) < PlayerPrefs.GetInt("rank" + (i + 1), 0))
                {
                    save1 = PlayerPrefs.GetInt("rank" + i, 0);
                    save2 = PlayerPrefs.GetInt("rank" + (i + 1), 0);
                    PlayerPrefs.SetInt("rank" + i, save2);
                    PlayerPrefs.SetInt("rank" + (i + 1), save1);
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
    void resultUpdate()
    {
        for (int i = 1; i < 11; i++)
        {
            texts[i - 1].text = i + " :" + PlayerPrefs.GetInt("rank" + (i - 1), 0);
        }
        text.text = "Score:" + save;
        high.text = "HighScore:" + PlayerPrefs.GetInt("HighScore", 0);
    }
}
