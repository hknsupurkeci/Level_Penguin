using System.Collections;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    public GameObject target;
    public float decreaseRate = 0.5f;  // Ne kadar hýzlý azalacaðý
    public float minimumScale = 0.1f;  // En düþük ölçek deðeri
    private bool isMoving=false;
    private Vector3 firstScale;
    private void Start()
    {
        firstScale = target.transform.localScale;
    }
    private void Update()
    {
        if (isMoving)
        {
            // GameObject'in x ölçeði minimumScale'den büyük olduðu sürece döngüye devam et
            if (target.transform.localScale.x > minimumScale)
            {
                // Yeni ölçeði hesapla
                Vector3 newScale = target.transform.localScale;
                newScale.x -= Time.deltaTime * decreaseRate;

                // Yeni ölçeði uygula
                target.transform.localScale = newScale;
            }
            else
            {
                IsMoving = false;
            }
        }
        else
        {
            target.transform.localScale = firstScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Scale trigger");
            // Eðer trigger'a baþka bir GameObject girerse scale azaltma iþlemine baþla
            IsMoving = true;
        }
    }
    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }
}
