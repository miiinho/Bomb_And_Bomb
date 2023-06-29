using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    public BombCtrl bombCtrl;
    public PlayerCtrl playerCtrl;
    public EnemyAI ai;
    public Renderer ren;

    public bool stop = false;

    void Start()
    {
        ren = GetComponent<MeshRenderer>();
        bombCtrl = GameObject.FindWithTag("BOMB").GetComponent<BombCtrl>();
        playerCtrl = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
        ai = GameObject.FindGameObjectWithTag("ENEMY").GetComponent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "BOX":
                stop = true;
                Destroy(other.gameObject);
                break;

            case "WALL":
                stop = true;
                break;

            case "BOMB":
                bombCtrl.CreateBombEffect();
                break;

            case "Player":
                playerCtrl.isDie = true;
                break;
        }
    }
}
