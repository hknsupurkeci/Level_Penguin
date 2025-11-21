using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawObstacleMovoment : MonoBehaviour
{
    public GameObject obstacle;
    public List<Transform> waypoints; // Gezinilecek transformlarýn listesi
    public float moveSpeed = 5f; // Hareket hýzý
    public float followDelay = 0.5f; // Takip gecikmesi
    private int currentIndex = 0; // Geçerli transform indexi
    private bool isMoving = false; // Hareketin baþlayýp baþlamadýðýný kontrol etmek için
    private Vector2 startPos;
    public bool followPlayer = false; // Karakteri takip etme durumu
    private GameObject player; // Oyuncu referansý
    private Vector3 targetPosition;

    private void Start()
    {
        startPos = obstacle.transform.position;
        player = GameObject.FindGameObjectWithTag("Player"); // Oyuncuyu bul
        targetPosition = obstacle.transform.position; // Baþlangýç hedef pozisyonu
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
                // Waypointlere doðru hareket
                Transform target = waypoints[currentIndex];
                obstacle.transform.position = Vector3.MoveTowards(obstacle.transform.position, target.position, moveSpeed * Time.deltaTime);

                // Hedefe ulaþtýðýmýzý kontrol et
                if (Vector3.Distance(obstacle.transform.position, target.position) < 0.1f)
                {
                    currentIndex = (currentIndex + 1) % waypoints.Count; // Bir sonraki transforma geç
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
            yield return new WaitForSeconds(followDelay); // Belirtilen süre kadar bekle
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
        if (collision.gameObject.CompareTag("Player")) // Çarpan objenin etiketi "Player" ise
        {
            Debug.Log("trigger baba");
            isMoving = true; // Hareketi baþlat
        }
    }
}
