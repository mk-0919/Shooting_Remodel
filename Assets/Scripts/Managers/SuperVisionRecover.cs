using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperVisionRecover : MonoBehaviour
{
    int currentRecoverVal = 0;
    public AudioSource picRecover;
    public AudioSource RecoverSE;
    [System.NonSerialized]public bool isRecover = false;

    GameObject SystemMesUI;
    TimerDisPlay TimerDisPlay;
    GameObject RecoverManagerObject;
    RecoverManager RecoverManager;

    public SuperVisionAmmo SuperVisionAmmo;

    private void Awake()
    {
        SystemMesUI = GameObject.Find("SystemMesUI");
        TimerDisPlay = SystemMesUI.GetComponent<TimerDisPlay>();
        RecoverManagerObject = GameObject.Find("RecoverManager");
        RecoverManager = RecoverManagerObject.GetComponent<RecoverManager>();
    }

    public void GetRecover()
    {
        currentRecoverVal += 1;
        DisplayUpdate();
        picRecover.Play();
    }
    public bool isRecoverUsable()
    {
        if (currentRecoverVal > 0 && isRecover == false && SuperVisionAmmo.isReloading == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UseRecover()
    {
        StartCoroutine("UseRecovery");
    }
    public void StopUseRecover()
    {
        StopCoroutine("UseRecovery");
        RecoverSE.Stop();
    }
    public void DisplayUpdate()
    {
        RecoverManager.DisPlayUpdate(currentRecoverVal);
    }
    private IEnumerator UseRecovery()
    {
        isRecover = true;
        currentRecoverVal -= 1;
        DisplayUpdate();
        RecoverSE.Play();
        TimerDisPlay.RecoverDisplay();
        yield return new WaitForSeconds(4);
        isRecover = false;
    }
}
