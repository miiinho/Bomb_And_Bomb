using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{


    void Start()
    {
        StartCoroutine(UICtrl());
    }

    IEnumerator UICtrl()
    {
        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }
}

