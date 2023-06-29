using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCtrl : MonoBehaviour
{
    public bool shoesOn = false;
    public bool dartsOn = false;
    public bool springOn = false;
    public bool isDie = false;

    public GameManager gameManager;
    private CharacterController characterController;
    public BombCtrl bombCtrl;
    public Transform playerTr;
    public Animator anim;

    public GameObject[] item = new GameObject[1];
    public GameObject Darts;
    public GameObject Spring;
    public GameObject Shield;
    public GameObject itemEatEffect;

    public Rigidbody rb;
    public AudioSource bombaudio;
    public AudioClip bombClip;
    public AudioClip itemClip;

    void Start()
    {
        bombaudio = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        playerTr = GetComponent<Transform>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.CreateBomb();        
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(h, 0, v);

        if (h == 0 && v == 0)
        {
            if(isDie == false)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Idle", true);
            }
        }

        else
        {
            if (isDie == false)
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", true);
            }
        }

        moveDir *= gameManager.moveSpeed;

        characterController.Move(moveDir * Time.deltaTime);

        if (h == 0 & v == 0)
        {
            return;
        }

        else
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDir);

            rb.MoveRotation(newRotation);
        }

        CheckArray();

        PlayerDie();
    }

    void PlayerDie()
    {
        if(isDie == true)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Die", true);

          
            gameManager.moveSpeed = 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "BOOSTER":
                bombaudio.clip = itemClip;
                bombaudio.Play();
                gameManager.Booster();
                Instantiate(itemEatEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
                break;

            case "MAXBOOSTER":
                bombaudio.clip = itemClip;
                bombaudio.Play();
                gameManager.MAXBOOSTER();
                Instantiate(itemEatEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
                break;

            case "BOMBCNT":
                bombaudio.clip = itemClip;
                bombaudio.Play();
                gameManager.BOMBCNT();
                Instantiate(itemEatEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
                break;

            case "BOMBLENGTH":
                bombaudio.clip = itemClip;
                bombaudio.Play();
                gameManager.BOMBLENGTH();
                Instantiate(itemEatEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
                break;

            case "MAXLENGTH":
                bombaudio.clip = itemClip;
                bombaudio.Play();
                gameManager.MAXLENGTH();
                Instantiate(itemEatEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
                break;

            case "DARTS":
                bombaudio.clip = itemClip;
                bombaudio.Play();
                if(dartsOn == true)
                {
                    gameManager.dartslimit = 3f;
                }
                item[0] = Darts;
                Instantiate(itemEatEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
                dartsOn = true;
                gameManager.DARTS();
                break;

            case "SPRING":
                bombaudio.clip = itemClip;
                bombaudio.Play();
                if (dartsOn == true)
                {
                    gameManager.springlimit = 3f;
                }
                item[0] = Spring;
                Instantiate(itemEatEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
                springOn = true;
                gameManager.SPRING();
                break;

            case "SHIELD":
                bombaudio.clip = itemClip;
                bombaudio.Play();
                item[0] = Shield;
                Instantiate(itemEatEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
                gameManager.SHIELD();
                break;
        }
    }

    public void CheckArray()
    {
        foreach(GameObject item in item)
        {
            if(item == Darts)
            {
                gameManager.DARTS();
            }
            if (item == Spring)
            {
                gameManager.SPRING();
            }
            if (item == Shield)
            {
                gameManager.SHIELD();
            }
        }
    }

}
