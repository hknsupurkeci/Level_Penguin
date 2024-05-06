using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyTrigger : MonoBehaviour
{
    [SerializeField] TransparencyController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Çarpan objenin etiketi "Player" ise
        {
            controller.TriggerTransparencyChange();
        }
    }
}
