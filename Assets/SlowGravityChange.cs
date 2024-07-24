using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowGravityChange : MonoBehaviour
{
    public bool slowGravity = false; // Bu de�i�ken, Unity Inspector �zerinden ayarlanabilir.
    public float targetGravityY = -1.0f; // Hedef yer�ekimi de�eri
    public float changeDuration = 1.0f; // De�i�ikli�in tamamlanma s�resi
    private float initialGravityY = -20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (slowGravity)
            {
                // slowGravity etkinse, yer�ekimini yava��a hedef de�ere do�ru de�i�tir
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

        Physics2D.gravity = new Vector2(0, target); // Son de�eri garanti alt�na al
    }
    private void ResetGravityToInitial()
    {
        Physics2D.gravity = new Vector2(0, initialGravityY);
    }
}
