using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCtrl : MonoBehaviour
{
    public GameObject effect;
    public GameManager gameManager;
    public DestroyBox destroyBox;
    public BombCtrl bombCtrl;

    public float effectSpeed = 50f;
    public float effectL = 1f;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        destroyBox = GetComponentInChildren<DestroyBox>();
    }

    void Update()
    {
        effectL += Time.deltaTime * effectSpeed;

        if (destroyBox.stop == true)
        {
            effectSpeed = 0;
        }

        if (effectL >= gameManager.effectMax)
        {
            effectL = gameManager.effectMax;
        }

        effect.transform.localScale = new Vector3(effectL, 1, 1);
    }

    
}
