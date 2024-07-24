using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawObstacleMovoment : MonoBehaviour
{
    public GameObject obstacle;
    public List<Transform> waypoints; // Gezinilecek transformlar�n listesi
    public float moveSpeed = 5f; // Hareket h�z�
    public float followDelay = 0.5f; // Takip gecikmesi
    private int currentIndex = 0; // Ge�erli transform indexi
    private bool isMoving = false; // Hareketin ba�lay�p ba�lamad���n� kontrol etmek i�in
    private Vector2 startPos;
    public bool followPlayer = false; // Karakteri takip etme durumu
    private GameObject player; // Oyuncu referans�
    private Vector3 targetPosition;

    private void Start()
    {
        startPos = obstacle.transform.position;
        player = GameObject.FindGameObjectWithTag("Player"); // Oyuncuyu bul
        targetPosition = obstacle.transform.position; // Ba�lang�� hedef pozisyonu
        if (followPlayer)
        {
            StartCoroutine(FollowPlayerWithDelay());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (followPlayer && player != null)
            {
                // Karakteri x ekseninde gecikmeli takip et
                obstacle.transform.position = Vector3.MoveTowards(obstacle.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else if (waypoints.Count > 0)
            {
                // Waypointlere do�ru hareket
                Transform target = waypoints[currentIndex];
                obstacle.transform.position = Vector3.MoveTowards(obstacle.transform.position, target.position, moveSpeed * Time.deltaTime);

                // Hedefe ula�t���m�z� kontrol et
                if (Vector3.Distance(obstacle.transform.position, target.position) < 0.1f)
                {
                    currentIndex = (currentIndex + 1) % waypoints.Count; // Bir sonraki transforma ge�
                }
                if (currentIndex == waypoints.Count)
                {
                    isMoving = false;
                }
            }
        }
    }

    private IEnumerator FollowPlayerWithDelay()
    {
        while (followPlayer)
        {
            if (player != null)
            {
                targetPosition = new Vector3(player.transform.position.x, obstacle.transform.position.y, obstacle.transform.position.z);
            }
            yield return new WaitForSeconds(followDelay); // Belirtilen s�re kadar bekle
        }
    }

    public Vector2 StartPosObstacle { get { return startPos; } }
    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }
    public int CurrentIndex
    {
        get { return currentIndex; }
        set { currentIndex = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �arpan objenin etiketi "Player" ise
        {
            Debug.Log("trigger baba");
            isMoving = true; // Hareketi ba�lat
        }
    }
}
