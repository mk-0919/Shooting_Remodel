using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyPlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currntHealth;
    public Slider HealthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public SuperVisionAmmo SuperVisionAmmo;
    public SuperVisionRecover SuperVisionRecover;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    public static bool isDead;
    bool damaged;
    MyPlayerShooting myPlayerShooting;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        currntHealth = startingHealth;
        myPlayerShooting = GetComponentInChildren<MyPlayerShooting>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed + Time.deltaTime);
        }
        damaged = false;

        if (Input.GetKeyDown(KeyCode.E) && SuperVisionRecover.isRecoverUsable() && currntHealth < 100)
        {
            StartCoroutine("UseRecover");
        }
    }
    private IEnumerator UseRecover()
    {
        SuperVisionRecover.UseRecover();
        yield return new WaitForSeconds(4);
        if (!isDead) Recover(20);
    }
    public void TakeDamage(int amount)
    {
        damaged = true;

        currntHealth -= amount;

        HealthSlider.value = currntHealth;

        playerAudio.Play();

        if (currntHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        myPlayerShooting.DisableEffects();
        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerMovement.enabled = false;
        myPlayerShooting.enabled = false;
        SuperVisionAmmo.StopReload();
        SuperVisionRecover.StopUseRecover();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void Recover(int val)
    {
        currntHealth += val;

        if (currntHealth >= 100)
        {
            currntHealth = 100;
        }

        HealthSlider.value = currntHealth;
    }
}
