using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressed = false;
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        playerController.StopMoving(); // Buton býrakýldýðýnda hareketi durdur
    }

    void Update()
    {
        if (isPressed)
        {
            // Hangi butona basýldýysa ona göre hareketi kontrol edin
            if (gameObject.name == "LeftButton")
            {
                playerController.MoveLeft();
            }
            else if (gameObject.name == "RightButton")
            {
                playerController.MoveRight();
            }
        }
    }
}
