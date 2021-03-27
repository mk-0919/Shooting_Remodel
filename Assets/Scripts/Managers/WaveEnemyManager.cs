using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum difficulty
{
    Normal,
    hard
}

public class WaveEnemyManager : MonoBehaviour
{
    public int AllSpawnNum = 0;
    public static int killCount = 0;
    int NowEnemyNum;
    public GameObject EnemyManager;
    MyEnemyManager[] myEnemyManagers;
    public Text Text;
    float r, g, b, a;
    public AudioSource AudioSource;
    public difficulty difficulty;
    public SuperVisionAmmo SuperVisionAmmo;
    public SuperVisionRecover SuperVisionRecover;

    private void Awake()
    {
        myEnemyManagers = EnemyManager.GetComponents<MyEnemyManager>();
        NowEnemyNum = WaveNumManager.WaveNum * 6 + 10;
        r = Text.color.r;
        g = Text.color.g;
        b = Text.color.b;
        a = Text.color.a;
        if(difficulty.ToString() == "hard")
        {
            foreach(MyEnemyManager x in myEnemyManagers)
            {
                x.spawnTime = x.spawnTime / 2;
            }
        }
    }
    private void Update()
    {
        AllSpawnNum = 0;
        foreach(MyEnemyManager x in myEnemyManagers)
        {
            AllSpawnNum += x.SpawnNum;
        }
        if (AllSpawnNum >= WaveNumManager.WaveNum * 6)
        {
            WaveLimit();
        }
        if (killCount >= NowEnemyNum)
        {
            WaveCountUp();
        }
    }
    private void WaveLimit()
    {
        NowEnemyNum = AllSpawnNum;
        foreach(MyEnemyManager x in myEnemyManagers)
        {
            x.StopSpawn();
        }
    }
    private void WaveCountUp()
    {
        killCount = 0;
        NowEnemyNum = WaveNumManager.WaveNum * 6 + 10;
        WaveNumManager.WaveNum++;
        StartCoroutine("CountUpText");
        WaveBonus();
        foreach (MyEnemyManager x in myEnemyManagers)
        {
            x.SpawnNum = 0;
            x.RestartSpawn();
        }
    }
    private IEnumerator CountUpText()
    {
        Text.text = "WAVE : " + WaveNumManager.WaveNum;
        Text.enabled = true;
        float Num = 0.01f;
        AudioSource.Play();
        for (int i = 0; i < 100; i++)
        {
            a += Num;
            Text.color = new Color(r, g, b, a);
            yield return new WaitForSeconds(Num);
        }
        for (int i = 0; i < 100; i++)
        {
            a -= Num;
            Text.color = new Color(r, g, b, a);
            yield return new WaitForSeconds(Num);
        }
        Text.enabled = false;
    }
    private void WaveBonus()
    {
        if (WaveNumManager.WaveNum % 3 == 0)
        {
            SuperVisionRecover.GetRecover();
        }
        else
        {
            SuperVisionAmmo.AddAmmo();
        }
    }
}
