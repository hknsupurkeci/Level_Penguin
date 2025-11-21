using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    public float defaultWidth = 15.2f;

    private Camera cam;
    private int lastScreenWidth;
    private int lastScreenHeight;

    void Awake()
    {
        cam = GetComponent<Camera>(); // PERFORMANS: Cache kamera
    }

    void Start()
    {
        AdjustCamera();
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
    }

    void Update()
    {
        // PERFORMANS SORUNU BURADA! Her frame ekran boyutunu kontrol ediyor
        // Sadece ekran boyutu DEÐÝÞTÝÐÝNDE çalýþtýr
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            AdjustCamera();
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
        }
    }

    void AdjustCamera()
    {
        if (cam == null) return;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = defaultWidth / cam.orthographicSize;

        if (screenRatio >= targetRatio)
        {
            cam.orthographicSize = defaultWidth / screenRatio;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            cam.orthographicSize *= differenceInSize;
        }
    }
}