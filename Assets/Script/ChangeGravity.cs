using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float gravityScale = collision.gameObject.GetComponent<Rigidbody2D>().gravityScale; // 2
            int scaleValue = gravityScale < 0 ? 2 : -2;
            PlayerController.gravityFlag = gravityScale < 0 ? 1 : -1;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = scaleValue;
            //Destroy(this.gameObject);
        }
    }
}