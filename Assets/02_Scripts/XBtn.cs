using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBtn : MonoBehaviour
{
    public GameObject xBtn;

    public void BtnOn()
    {
        xBtn.SetActive(false);
    }
}
