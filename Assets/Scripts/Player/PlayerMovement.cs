using UnityEngine;
using System.Threading.Tasks;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6f;
    float speed, slowspeed;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    MyPlayerHealth MyPlayerHealth;
    int floorMask;
    float camRayLength = 100f;
    public SuperVisionAmmo SuperVisionAmmo;
    public SuperVisionRecover SuperVisionRecover;
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");

        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        MyPlayerHealth = GetComponent<MyPlayerHealth>();
        speed = Speed;
        slowspeed = Speed / 2;
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

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
}
