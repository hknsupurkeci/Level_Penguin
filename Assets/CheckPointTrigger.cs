using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField] int CheckPointId;
    [SerializeField] GameObject checkPointEffect;
    [SerializeField] GameObject checkPointEffectLight;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (PlayerController.activeCheckPointId < CheckPointId)
            {
                PlayerController.activeCheckPointId = CheckPointId;
                PlayerPrefs.SetInt("checkpointId", CheckPointId);
            }
            Instantiate(checkPointEffect, transform.position, Quaternion.identity);
            Instantiate(checkPointEffectLight, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
