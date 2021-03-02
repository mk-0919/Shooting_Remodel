using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MyPlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    PlayerMovement PlayerMovement;
    [SerializeField] private AudioSource gunAudio;
    [SerializeField] private AudioSource noAmmo;
    public SuperVisionAmmo SuperVisionAmmo;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        PlayerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && SuperVisionAmmo.SuperVisionRecover.isRecover == false && StartProcess.processPermit)
        {
            if (SuperVisionAmmo.shooting() == 1)
            {
                Shoot();
            }
            else
            {
                noAmmo.Play();
                SuperVisionAmmo.Reload();
            }
            
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SuperVisionAmmo.Reload();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            MyEnemyHealth myEnemyHealth = shootHit.collider.GetComponent<MyEnemyHealth>();
            if (myEnemyHealth != null)
            {
                myEnemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
