using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Karakterimizin transformu
    public float smoothSpeed = 0.125f; // Kameranýn ne kadar pürüzsüz hareket edeceðini belirleyen faktör
    public float yOffset = 1f; // Kameranýn karakterin üzerinde ne kadar yüksek duracaðýný belirleyen offset

    private void LateUpdate()
    {
        if (target != null)
        {
            // Hedefin Y pozisyonunu al ve yOffset ile ayarla
            float desiredY = target.position.y + yOffset;
            // Kameranýn þu anki Y pozisyonunu al
            float currentY = transform.position.y;
            // Ýstenen Y pozisyonuna doðru pürüzsüz bir geçiþ yap
            float smoothedY = Mathf.Lerp(currentY, desiredY, smoothSpeed * Time.deltaTime);
            // Kameranýn pozisyonunu güncelle, X ve Z pozisyonlarý sabit kalacak
            transform.position = new Vector3(transform.position.x, smoothedY, transform.position.z);
        }
    }
}
