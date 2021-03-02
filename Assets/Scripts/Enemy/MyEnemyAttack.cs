using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    Animator anim;
    GameObject player;
    MyPlayerHealth myPlayerHealth;
    bool playerInRange;
    float timer;
    MyEnemyHealth myEnemyHealth;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myPlayerHealth = player.GetComponent<MyPlayerHealth>();
        anim = GetComponent<Animator>();
        myEnemyHealth = GetComponent<MyEnemyHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && myEnemyHealth.currentHealth>0)
        {
            Attack();
        }

        if (myPlayerHealth.currntHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }
    void Attack()
    {
        timer = 0f;
        
        if (myPlayerHealth.currntHealth > 0)
        {
            myPlayerHealth.TakeDamage(attackDamage);
        }
    }
}
