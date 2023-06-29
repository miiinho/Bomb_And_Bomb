using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBtn : MonoBehaviour
{
    public GameObject returnBtn;

    public void RBtn()
    {
        returnBtn.SetActive(false);
    }
}
