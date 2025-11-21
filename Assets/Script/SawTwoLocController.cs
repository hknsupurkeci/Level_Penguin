using System.Collections;
using UnityEngine;

public class SawTwoLocController : MonoBehaviour
{
    private Vector3 startPoz;
    public Transform endPoz;
    public float startWaitTime = 1f; // Start pozisyonunda bekleme süresi
    public float endWaitTime = 2f;   // End pozisyonunda bekleme süresi
    public float sawSpeed = 5f;      // Hareket hýzý
    private bool movingToEnd = true; // Hedefe doðru mu geriye mi hareket ediyor

    void Start()
    {
        startPoz = transform.position; // Baþlangýç pozisyonunu kaydet
        StartCoroutine(MoveBetweenPositions()); // Hareketi baþlat
    }

    IEnumerator MoveBetweenPositions()
    {
        Vector3 targetPoz = endPoz.position;
        float currentWaitTime = endWaitTime;

        while (true)
        {
            // Hedefe doðru sürekli hareket
            while (Vector3.Distance(transform.position, targetPoz) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPoz, sawSpeed * Time.deltaTime);
                yield return null;
            }

            // Hedefe ulaþýnca belirlenen süre kadar bekle
            yield return new WaitForSeconds(currentWaitTime);

            // Hedef pozisyonunu ve bekleme süresini deðiþtir
            if (movingToEnd)
            {
                targetPoz = startPoz;
                currentWaitTime = startWaitTime;
            }
            else
            {
                targetPoz = endPoz.position;
                currentWaitTime = endWaitTime;
            }

            movingToEnd = !movingToEnd; // Hareket yönünü deðiþtir
        }
    }
}
