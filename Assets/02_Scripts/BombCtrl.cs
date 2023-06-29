using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCtrl : MonoBehaviour
{
    public GameObject bomb;
    public GameObject bombEffect;
    public GameObject bombEffect2;

    public Renderer bombRend;

    public GameManager gameManager;
    public Transform bombTr;
    public Transform playerTr;
    public SphereCollider bombCollider;
    public PlayerCtrl playerCtrl;
    public GameObject player;
    public GameObject explosion;

    public float effectSize = 1f;
    public float effectMax = 5f;
    public float force = 100f;
    public float forceDistance = 1f;
    public float pbDistance = 2f;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bombRend = GetComponent<MeshRenderer>();
        playerTr = player.GetComponent<Transform>();
        playerCtrl = player.GetComponent<PlayerCtrl>();
        bombCollider = GetComponent<SphereCollider>();
        bombTr = GetComponent<Transform>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        StartCoroutine(BombEffectDelay());
    }

    IEnumerator BombEffectDelay()
    {
        yield return new WaitForSeconds(3f);

        CreateBombEffect();
    }

    public void CreateBombEffect()
    {
        bombRend.enabled = false;

        bombEffect.SetActive(true);
        bombEffect2.SetActive(false);

        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject, 0.5f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DARTSITEM")
        {
            Destroy(other.gameObject);
            CreateBombEffect();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.AddForce(collision.transform.forward * force);
        }
        else if(collision.gameObject.tag == "BOX")
        {           
            rb.velocity = Vector3.zero;
        }
        else if (collision.gameObject.tag == "BOMB")
        {
            rb.velocity = Vector3.zero;

        }
    }
}
