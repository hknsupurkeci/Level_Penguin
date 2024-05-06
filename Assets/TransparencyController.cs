using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    public List<GameObject> gameObjects; // GameObject listesi
    private int currentObjectIndex = 0; // Þu anki GameObject index'i
    private Coroutine coroutine;
    void Start()
    {
        ResetTransparency(); // Baþlangýçta tüm objeleri þeffaf yap
    }

    public void OnPlayerDeath()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine); // Çalýþan Coroutine'i durdur
            coroutine = null; // Coroutine referansýný temizle
        }
        ResetTransparency();
        currentObjectIndex = 0; // Index'i sýfýrla ama þeffaflýk deðiþimini baþlatma
    }


    private void ResetTransparency()
    {
        // Tüm objelerin renklerini tamamen þeffaf yap
        foreach (GameObject obj in gameObjects)
        {
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                Color color = renderer.color;
                color.a = 0;
                renderer.color = color;
            }
        }
    }

    // Trigger fonksiyonu, dýþarýdan tetiklenebilir
    public void TriggerTransparencyChange()
    {
        if (currentObjectIndex < gameObjects.Count)
        {
            coroutine = StartCoroutine(ChangeTransparency(gameObjects[currentObjectIndex]));
        }
    }

    IEnumerator ChangeTransparency(GameObject obj)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            while (renderer.color.a < 1)
            {
                Color color = renderer.color;
                color.a += Time.deltaTime / 2f; // Her saniyede renk deðiþim hýzý
                renderer.color = color;
                yield return null;
            }
            currentObjectIndex++; // Sonraki objeye geç
            if (currentObjectIndex < gameObjects.Count)
            {
                TriggerTransparencyChange(); // Otomatik olarak sonraki objeyi tetikle
            }
        }
    }
}
