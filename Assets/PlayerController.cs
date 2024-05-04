using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7.0f;
    public float isJumpSpeed = 4.0f;
    private float firstSpeed;
    public float jumpForce = 10f;  // Doðrudan Y ekseninde hýz vereceðimiz için düþük bir deðer yeterli olacak
    public float rotationSpeed = 150f; // Karakterin dönme hýzýný belirleyen parametre
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isWall = false;

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
        firstSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;  // Yer çekimi kuvvetini artýrarak daha gerçekçi bir düþüþ saðlýyoruz
    }

    void Update()
    {
        // Zýplama kontrolü
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * gravityFlag); // Doðrudan y ekseninde hýz vererek zýplama
            speed = isJumpSpeed;
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
        // Duvara temas
        if (collision.gameObject.tag == "Wall")
        {
            // wall false ise bir kerelik duvardan atlayabilecek daha sonra isWall sadece ground deðdiðinde false olacaðý için tekrardan zýplayamayacak.
            if(!isWall)
            {
                isGrounded = true;
            }
            isWall = true;
        }
        // Zemin ile temasý kontrol et
        if (collision.gameObject.tag == "Ground")
        {
            speed = firstSpeed;
            isGrounded = true;
            isWall = false;
            transform.rotation = Quaternion.identity;  // Karakter yere temas ettiðinde rotasyonu sýfýrla
        }
        if (collision.gameObject.tag == "Obstackle")
        {
            // karakter parçalanma ve ilgili checkpointe gitme
            gameObject.GetComponent<FragmentController>().Disassemble();
            // eðer karakter ölürse otomatik olarak yer çekimi normale dönecek.
            rb.gravityScale = 2;
            gravityFlag = 1;
            Debug.Log("dead");
        }
    }

}
