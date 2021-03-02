using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperVisionAmmo : MonoBehaviour
{
    int ammo;
    int RemainingAmmo;
    public AudioSource picAmmo;
    public AudioSource ReLoad;
    [System.NonSerialized]public bool isReloading = false;

    GameObject SystemMesUI;
    TimerDisPlay TimerDisPlay;
    GameObject AmmoManagerObject;
    AmmoManager AmmoManager;

    public SuperVisionRecover SuperVisionRecover;

    void Awake()
    {
        ammo = 30;
        RemainingAmmo = 100;
        SystemMesUI = GameObject.Find("SystemMesUI");
        TimerDisPlay = SystemMesUI.GetComponent<TimerDisPlay>();
        AmmoManagerObject = GameObject.Find("AmmoManager");
        AmmoManager = AmmoManagerObject.GetComponent<AmmoManager>();
    }
    public void AddAmmo()
    {
        int rnd = Random.Range(10, 20);
        RemainingAmmo += rnd;
        DisplayUpdate();
        picAmmo.Play();
    }
    public int shooting()
    {
        if (ammo >= 1)
        {
            ammo--;
            DisplayUpdate();
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public void Reload()
    {
        if(isReloading == false && RemainingAmmo > 0 && ammo != 30 && SuperVisionRecover.isRecover == false)
        {
            StartCoroutine("Reloading");
        }
    }
    public void StopReload()
    {
        StopCoroutine("Reloading");
        ReLoad.Stop();
    }
    public void DisplayUpdate()
    {
        AmmoManager.DisPlayUpdate(ammo, RemainingAmmo);
    }
    private IEnumerator Reloading()
    {
        isReloading = true;
        int currentAmmo = ammo;
        ammo = 0;
        ReLoad.Play();
        DisplayUpdate();
        TimerDisPlay.ReloadDisplay();
        yield return new WaitForSeconds(3);
        if (currentAmmo + RemainingAmmo <= 30)
        {
            ammo = currentAmmo + RemainingAmmo;
        }
        else
        {
            ammo = 30;
        }
        RemainingAmmo -= (30 - currentAmmo);
        if (RemainingAmmo <= 0) RemainingAmmo = 0;
        DisplayUpdate();
        isReloading = false;
    }
}
