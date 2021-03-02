using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecoverManager : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }
    public void DisPlayUpdate(int currentRecoverVal)
    {
        text.text = ": " + currentRecoverVal;
    }
}
