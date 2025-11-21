using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7.0f;
    public float isJumpSpeed = 4.0f;
    private float firstSpeed;
    public float jumpForce = 10f;
    public float rotationSpeed = 150f;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isWall = false;
    public static int activeCheckPointId;
    public CheckPoints checkPointController;
    public static int gravityFlag = 1;

    private void Awake()
    {
        activeCheckPointId = PlayerPrefs.GetInt("checkpointId", 0);
        transform.position = checkPointController.points[activeCheckPointId].position;
    }

    void Start()
    {
        firstSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
    }

    public void MoveLeft()
    {
        rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    public void MoveRight()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }

    public void StopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    // Zıplama fonksiyonu - tıkladığınız anda çalışacak
    public void Jump()
    {
        if (isGrounded)
        {
            SoundsManager.Instance.PlaySound(SoundsManager.Instance.jumpSounds[UnityEngine.Random.Range(0, 2)], 0.2f);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * gravityFlag);
            speed = isJumpSpeed;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (!isWall)
            {
                isGrounded = true;
            }
            isWall = true;
        }

        if (collision.gameObject.tag == "Ground")
        {
            speed = firstSpeed;
            isGrounded = true;
            isWall = false;
            transform.rotation = Quaternion.identity;
        }

        if (collision.gameObject.tag == "Obstackle")
        {
            SoundsManager.Instance.PlaySound(SoundsManager.Instance.deadSound, 0.2f);
            gameObject.GetComponent<FragmentController>().Disassemble();
            rb.gravityScale = 2;
            gravityFlag = 1;
            Debug.Log("dead");
        }
    }
}