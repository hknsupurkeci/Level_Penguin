using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawObstacleMovoment : MonoBehaviour
{
    public GameObject obstacle;
    public List<Transform> waypoints; // Gezinilecek transformlarýn listesi
    public float moveSpeed = 5f; // Hareket hýzý
    private int currentIndex = 0; // Geçerli transform indexi
    private bool isMoving = false; // Hareketin baþlayýp baþlamadýðýný kontrol etmek için
    private Vector2 startPos;

    private void Start()
    {
        startPos = obstacle.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        // Transforma doðru hareket
        if (isMoving)
        {
            if (waypoints.Count > 0)
            {
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
