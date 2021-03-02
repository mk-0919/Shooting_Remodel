using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyMovement : MonoBehaviour
{
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    MyPlayerHealth myPlayerHealth;
    MyEnemyHealth myEnemyHealth;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        myPlayerHealth = player.GetComponent<MyPlayerHealth>();
        myEnemyHealth = GetComponent<MyEnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myEnemyHealth.currentHealth > 0 && myPlayerHealth.currntHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
