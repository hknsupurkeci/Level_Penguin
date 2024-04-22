using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FragmentController FragmentController;
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            FragmentController.StartReassemble();
        }
    }
}
