using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Ammo,
        Recover
    }

    public ItemType type;

    public float deleteTime;

    void Start()
    {
        StartCoroutine("DeleteItem");
    }

    IEnumerator DeleteItem()
    {
        yield return new WaitForSeconds(deleteTime);
        Destroy(this.gameObject);
    }
}
