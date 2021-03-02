using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    public Item item1;
    public Item item2;
    public float dropRate;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }
        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        Invoke("ItemDrop", 1.0f);
    }
    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        MyScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }

    void ItemDrop()
    {
        float rnd = Random.Range(0, 1f);
        var vec = new Vector3(0, 1.5f, 0);

        if (rnd < dropRate)
        {
            if (rnd < 0.8)
            {
                var dropItem = Instantiate(item1, this.transform.position + vec, this.transform.rotation);
                dropItem.transform.GetComponent<Animation>().Play();
            }
            else
            {
                var dropItem = Instantiate(item2, this.transform.position + vec, this.transform.rotation);
                dropItem.transform.Rotate(new Vector3(0, 1, 0), 180);
                dropItem.transform.GetComponent<Animation>().Play();
            }
        }
    }
}
