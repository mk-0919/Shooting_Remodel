using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    Vector3 PlayerMovement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");

        PlayerMovement.Set(0, 0, 0);
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void PlayerMove(float Sec)
    {
        StartCoroutine(MoveControl(Sec));
    }

    private IEnumerator MoveControl(float Sec)
    {
        PlayerMovement = Vector3.back;
        anim.SetBool("IsWalking", true);
        yield return new WaitForSeconds(Sec);
        PlayerMovement.Set(0, 0, 0);
        anim.SetBool("IsWalking", false);
    }

    private void Move()
    {
        PlayerMovement = PlayerMovement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + PlayerMovement);
    }
}
