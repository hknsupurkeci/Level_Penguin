using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float yOffset = 1f;

    private Vector3 velocity = Vector3.zero;
    private Transform cachedTransform; // Cache transform

    void Awake()
    {
        cachedTransform = transform; // PERFORMANS: Transform cache
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // PERFORMANS: Vector3 nesne oluþturmayý azalt
        Vector3 currentPos = cachedTransform.position;
        Vector3 desiredPosition = new Vector3(currentPos.x, target.position.y + yOffset, currentPos.z);

        // SmoothDamp iyi bir seçim, Lerp'ten daha doðal
        cachedTransform.position = Vector3.SmoothDamp(currentPos, desiredPosition, ref velocity, smoothSpeed);
    }
}