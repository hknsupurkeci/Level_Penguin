using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public GameObject obstacle; // Hareket ettirilecek engel objesi
    [SerializeField] private float speed = 10.0f; // Hareket hýzý
    private bool isMoving = false; // Hareketin baþlayýp baþlamadýðýný kontrol etmek için
    private Vector2 startPos;

    public Transform targetPos;

    public bool flagXY = false; // false x , true y
    private void Start()
    {
        startPos = obstacle.transform.position;
    }
    private void Update()
    {
        if (isMoving)
        {
            Vector3 targetPosition = Vector3.zero;
            if(flagXY)
            {
                float targetY = targetPos != null ? targetPos.position.y : transform.position.y;
                targetPosition = new Vector3(obstacle.transform.position.x, targetY, obstacle.transform.position.z);
            }
            else
            {
                float targetX = targetPos != null ? targetPos.position.x : transform.position.x;
                targetPosition = new Vector3(targetX, obstacle.transform.position.y, obstacle.transform.position.z);
            }
            obstacle.transform.position = Vector3.MoveTowards(obstacle.transform.position, targetPosition, speed * Time.deltaTime);

            if (obstacle.transform.position == targetPosition)
            {
                isMoving = false; // Hedefe ulaþýldýðýnda hareketi durdur
            }
        }
    }

    public Vector2 StartPosObstacle {  get { return startPos; } }
    public bool IsMoving { 
        get {  return isMoving; } 
        set { isMoving = value; } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Çarpan objenin etiketi "Player" ise
        {
            Debug.Log("trigger");
            isMoving = true; // Hareketi baþlat
        }
    }
}
