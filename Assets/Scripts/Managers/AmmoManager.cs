using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    public Text ammoText;
    public Text remainingText;
    public void DisPlayUpdate(int ammo,int RemainingAmmo)
    {
        ammoText.text = "Ammo: " + ammo;
        remainingText.text = RemainingAmmo.ToString();
    }
}
