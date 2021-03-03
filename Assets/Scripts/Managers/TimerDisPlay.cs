using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDisPlay : MonoBehaviour
{
    [SerializeField] GameObject Reload;
    [SerializeField] GameObject Recover;
    ReloadTimer ReloadTimer;
    RecoverTimer RecoverTimer;

    private void Awake()
    {
        ReloadTimer = Reload.GetComponentInChildren<ReloadTimer>();
        RecoverTimer = Recover.GetComponentInChildren<RecoverTimer>();
    }
    private void Update()
    {
        if (MyPlayerHealth.isDead)
        {
            Reload.SetActive(false);
            Recover.SetActive(false);
        }
    }
    public void ReloadDisplay()
    {
        StartCoroutine("ReloadDisplayMain");
    }
    private IEnumerator ReloadDisplayMain()
    {
        Reload.SetActive(true);
        ReloadTimer.ReloadCount();
        yield return new WaitForSeconds(3);
        Reload.SetActive(false);
    }
    public void RecoverDisplay()
    {
        StartCoroutine("RecoverDisplayMain");
    }
    private IEnumerator RecoverDisplayMain()
    {
        Recover.SetActive(true);
        RecoverTimer.RecoverCount();
        yield return new WaitForSeconds(4.3f);
        Recover.SetActive(false);
    }
}
