using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform teleportLocation;
    public GameObject teleportEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(teleportEffect, this.transform.position, Quaternion.identity);
            collision.gameObject.transform.position = teleportLocation.position;
            // Rigidbody2D bile�enini al
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            // H�z� ve a��sal h�z� s�f�rla
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }
}
