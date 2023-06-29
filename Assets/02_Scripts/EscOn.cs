using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscOn : MonoBehaviour
{
    public GameObject escOn;
    public GameObject xBtn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escOn.SetActive(true);
        }
    }

    public void BtnOn()
    {
        escOn.SetActive(false);
    }
}
