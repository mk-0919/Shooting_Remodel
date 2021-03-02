using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async void ReloadDisplay()
    {
        Reload.SetActive(true);
        ReloadTimer.ReloadCount();
        await Task.Delay(3000);
        Reload.SetActive(false);
    }
    public async void RecoverDisplay()
    {
        Recover.SetActive(true);
        RecoverTimer.RecoverCount();
        await Task.Delay(4300);
        Recover.SetActive(false);
    }
}
