using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCtrl : MonoBehaviour
{
    public EnemyAI enemyAI;

    void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    void Update()
    {
        if (enemyAI.isDie)
        {
            gameObject.SetActive(false);
        }
    }
}
