using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowGravityChange : MonoBehaviour
{
    public bool slowGravity = false; // Bu deðiþken, Unity Inspector üzerinden ayarlanabilir.
    public float targetGravityY = -1.0f; // Hedef yerçekimi deðeri
    public float changeDuration = 1.0f; // Deðiþikliðin tamamlanma süresi
    private float initialGravityY = -20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (slowGravity)
            {
                // slowGravity etkinse, yerçekimini yavaþça hedef deðere doðru deðiþtir
                StartCoroutine(ChangeGlobalGravity(targetGravityY, changeDuration));
            }
            else
            {
                ResetGravityToInitial();
            }
        }
    }

    private IEnumerator ChangeGlobalGravity(float target, float duration)
    {
        float elapsed = 0.0f;
        float startGravityY = Physics2D.gravity.y;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newGravityY = Mathf.Lerp(startGravityY, target, elapsed / duration);
            Physics2D.gravity = new Vector2(0, newGravityY);
            yield return null;
        }

        Physics2D.gravity = new Vector2(0, target); // Son deðeri garanti altýna al
    }
    private void ResetGravityToInitial()
    {
        Physics2D.gravity = new Vector2(0, initialGravityY);
    }
}
