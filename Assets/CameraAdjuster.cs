using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    public float defaultWidth = 15.2f;  // Tasarýmýnýzý yaptýðýnýz varsayýlan geniþlik

    void Start()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        Camera cam = GetComponent<Camera>();
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

    // Ekran boyutu deðiþtiðinde kamerayý yeniden ayarlamak için
    void Update()
    {
        AdjustCamera();
    }
}
