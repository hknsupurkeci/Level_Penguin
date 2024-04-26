using UnityEngine;

public class FragmentController : MonoBehaviour
{
    public GameObject fragmentPrefab; // Parça prefabý
    public int fragmentsPerSide = 3; // Bir kenardaki parça sayýsý

    private Vector3 originalScale;

    public CheckPoints checkPointController;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void Disassemble()
    {
        for (int x = 0; x < fragmentsPerSide; x++)
        {
            for (int y = 0; y < fragmentsPerSide; y++)
            {
                GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);
                fragment.transform.localScale = originalScale / fragmentsPerSide;
                Vector2 direction = new Vector2(x - fragmentsPerSide / 2, y - fragmentsPerSide / 2).normalized;
                fragment.GetComponent<Rigidbody2D>().AddForce(direction * 5f, ForceMode2D.Impulse);
            }
        }
        gameObject.SetActive(false);
        Invoke("StartReassemble", 1f); // 1 saniye sonra yok etme
    }

    public void StartReassemble()
    {
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("PlayerPiece"))
        {
            Destroy(piece);
        }
        // engellerin posizyonunu sýfýrlar
        RestartGame();
        // son checkpointe gider
        transform.position = checkPointController.points[PlayerController.activeCheckPointId].position;
        gameObject.SetActive(true);
    }
    void RestartGame()
    {
        foreach (ObstacleMovement obs in FindObjectsOfType<ObstacleMovement>())
        {
            obs.obstacle.transform.position = obs.StartPosObstacle;
            obs.IsMoving = false;
            // Burada engelin aktiflik durumunu ayarlayabilirsiniz
        }
        foreach (SawObstacleMovoment obs in FindObjectsOfType<SawObstacleMovoment>())
        {
            obs.obstacle.transform.position = obs.StartPosObstacle;
            obs.IsMoving = false;
            obs.CurrentIndex = 0;
            // Burada engelin aktiflik durumunu ayarlayabilirsiniz
        }
    }
}
