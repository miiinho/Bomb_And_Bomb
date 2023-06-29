using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombRen : MonoBehaviour
{
    public MeshRenderer ren;
    public BombCtrl bombCtrl;

    void Start()
    {
        bombCtrl = GetComponentInParent<BombCtrl>();
        ren = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (bombCtrl.bombRend.enabled == false)
        {
            ren.enabled = false;
        }
    }
}
