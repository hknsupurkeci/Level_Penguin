using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10f;  // Doðrudan Y ekseninde hýz vereceðimiz için düþük bir deðer yeterli olacak
    public float rotationSpeed = 150f; // Karakterin dönme hýzýný belirleyen parametre
    private Rigidbody2D rb;
    private bool isGrounded = true;

    public static int activeCheckPointId;

    public CheckPoints checkPointController;

    public static int gravityFlag = 1; // burada yerçekimi deðiþince hangi tarafa zýplamasýný gerekdiðini aþaðýda çarpýyor
    private void Awake()
    {
        activeCheckPointId = PlayerPrefs.GetInt("checkpointId", 0);
        transform.position = checkPointController.points[activeCheckPointId].position;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;  // Yer çekimi kuvvetini artýrarak daha gerçekçi bir düþüþ saðlýyoruz
    }

    void Update()
    {
        // Zýplama kontrolü
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * gravityFlag); // Doðrudan y ekseninde hýz vererek zýplama
            isGrounded = false;
        }

        // Karakter dönüþü ve hareket kontrolü
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            transform.Rotate(0, 0, -moveInput * rotationSpeed * Time.deltaTime); // Zýplama sýrasýnda karakteri döndür
        }
        else if (isGrounded)
        {
            // Eðer hiçbir yatay tuþa basýlmýyorsa ve karakter yerdeyse, karakterin yatay hýzýný sýfýrla
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Zemin ile temasý kontrol et
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            transform.rotation = Quaternion.identity;  // Karakter yere temas ettiðinde rotasyonu sýfýrla
        }
        if (collision.gameObject.tag == "Obstackle")
        {
            // karakter parçalanma ve ilgili checkpointe gitme
            gameObject.GetComponent<FragmentController>().Disassemble();

            Debug.Log("dead");
        }
    }

}
