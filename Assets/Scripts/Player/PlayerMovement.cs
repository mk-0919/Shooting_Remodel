﻿using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6f;
    float speed, slowspeed, dashVal=30f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    MyPlayerHealth MyPlayerHealth;
    int floorMask;
    float camRayLength = 100f;
    public SuperVisionAmmo SuperVisionAmmo;
    public SuperVisionRecover SuperVisionRecover;
    bool isDash = false;
    public Slider staminaSlider;
    static float dashCount = 3;
    bool isStaDown = false, isStaUp = true;
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");

        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        MyPlayerHealth = GetComponent<MyPlayerHealth>();
        speed = Speed;
        slowspeed = Speed / 2;
        staminaSlider.value = dashVal;
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        staminaSlider.value = dashVal;

        if (StartProcess.processPermit)
        {
            Move(h, v);
            Animating(h, v);
        }
        Turning();
        if(SuperVisionAmmo.isReloading || SuperVisionRecover.isRecover)
        {
            speed = slowspeed;
        }
        else
        {
            speed = Speed;
        }

        if (Input.GetKey(KeyCode.LeftShift) && StartProcess.processPermit && dashVal > 0)
        {
            if (isStaDown == false && SuperVisionAmmo.isReloading == false && SuperVisionRecover.isRecover == false)
            {
                Speed = 12;
                StopCoroutine("staminaUp");
                isStaUp = false;
                StartCoroutine("staminaDown");
                isStaDown = true;
            }
        }
        else if(StartProcess.processPermit)
        {
            Speed = 6;
            StopCoroutine("staminaDown");
            isStaDown = false;
            if (dashVal < 30 && isStaUp == false)
            {
                dashCount = 3;
                StartCoroutine("staminaUp");
                isStaUp = true;
            }
        }
    }
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }
    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    public void getItem(Collider col)
    {
        var item = col.transform.GetComponent<Item>();

        if (item.type == Item.ItemType.Ammo)
        {
            SuperVisionAmmo.AddAmmo();
        }
        if (item.type == Item.ItemType.Recover)
        {
            SuperVisionRecover.GetRecover();
        }

        Destroy(col.gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Item")
        {
            getItem(col);
        }
    }
    private IEnumerator staminaDown()
    {
        while (dashVal > 0)
        {
            dashVal -= 0.25f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    private IEnumerator staminaUp()
    {
        while (dashCount > 0)
        {
            dashCount -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        if(dashCount <= 0)
        {
            while (dashVal <= 30)
            {
                dashVal += 0.2f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
