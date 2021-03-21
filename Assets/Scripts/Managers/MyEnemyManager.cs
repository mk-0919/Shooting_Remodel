using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyEnemyManager : MonoBehaviour
{
    public MyPlayerHealth myPlayerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public int SpawnNum = 0;

    void Awake()
    {
        spawnTime += 3;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Spawn()
    {
        if (myPlayerHealth.currntHealth <= 0f)
        {
            return;
        }
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        SpawnNum++;
    }
    public void StopSpawn()
    {
        CancelInvoke();
    }
    public void RestartSpawn()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
}
