using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    RaycastHit[] hits;

    public float moveSpeed;
    public float moveMaxSpeed;
    public float bombLimit;
    public float bombMaxLimit;
    public float bombCnt;
    public float effectMaxLength;
    public float effectMax;
    public float force = 50;
    private float curT;
    private float shieldTime = 2.5f;
    public float dartslimit = 3f;
    public float springlimit = 3f;

    public GameObject dartsPrefab;

    public CapsuleCollider playerCollider;
    public PlayerCtrl playerCtrl;
    public Transform playerTr;
    public Transform bombTr;
    public GameObject bombPrefab;
    public ObjectFind objectFind;
    public CharacterChoice cc;
    public GameObject player;
    public Rigidbody rb;
    public GameObject[] enemys;
    public GameResult result;
    public GameObject springEffect;
    public GameObject shieldEffect;

    public AudioSource itemsAudio;
    public AudioClip shieldAudio;
    public AudioClip springAudio;

    private bool isShield = false;

    void Start()
    {
        result = GetComponent<GameResult>();
        playerTr = player.GetComponent<Transform>();
        playerCtrl = player.GetComponent<PlayerCtrl>();
        playerCollider = player.GetComponent<CapsuleCollider>();
        rb = player.GetComponent<Rigidbody>();
        cc = GameObject.Find("CharacterChoice").GetComponent<CharacterChoice>();

        FirstCharacter();
        SecondCharacter();
        ThirdCharacter();
    }

    void Update()
    {

        if (isShield == true)
        {
            curT += Time.deltaTime;

            if(curT <= shieldTime)
            {
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Bomb"), true);
            }

            else if(curT > shieldTime)
            {
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Bomb"), false);

                isShield = false;
                curT = 0;
            }
        }

        if(result.end == true)
        {
            moveSpeed = 0;
        }

        EnemyCnt();
    }
    public void CreateBomb()
    {
        if (bombCnt < bombLimit && bombCnt >= 0)
        {
            Vector3 bombPos = new Vector3(playerTr.position.x, 0.2f, playerTr.position.z);
            GameObject bomb = Instantiate(bombPrefab, bombPos, Quaternion.Euler(0, 0, 0));
            playerCtrl.bombaudio.clip = playerCtrl.bombClip;
            playerCtrl.bombaudio.Play();

            ++bombCnt;

            StartCoroutine(BombCnt());
        }
    }
    public void FirstCharacter()
    {
        if (cc.isFirst == true)
        {
            moveSpeed = 5f;
            moveMaxSpeed = 9f;
            bombLimit = 1f;
            bombMaxLimit = 6f;
            bombCnt = 0;
            effectMax = 1f;
            effectMaxLength = 7f;
        }
    }


    public void SecondCharacter()
    {
        if (cc.isSecond == true)
        {
            moveSpeed = 5f;
            moveMaxSpeed = 8f;
            bombLimit = 1f;
            bombMaxLimit = 6f;
            bombCnt = 0;
            effectMax = 2f;
            effectMaxLength = 7f;
        }
    }

    public void ThirdCharacter()
    {
        if (cc.isThird == true)
        {
            moveSpeed = 5f;
            moveMaxSpeed = 7f;
            bombLimit = 1f;
            bombMaxLimit = 10f;
            bombCnt = 0;
            effectMax = 1f;
            effectMaxLength = 7f;
        }

    }

    public void EnemyCnt()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[0] == null && enemys[1] == null && enemys[2] == null && enemys[3] == null && enemys[4] == null)
            {
                result.isWin = true;
            }
        }
    }


    IEnumerator BombCnt()
    {
        yield return new WaitForSeconds(3f);

        bombCnt--;
    }

    #region item
    public void Booster()
    {
        moveSpeed += 2f;
        if (moveSpeed >= moveMaxSpeed)
        {
            moveSpeed = moveMaxSpeed;
        }
    }

    public void MAXBOOSTER()
    {
        moveSpeed = moveMaxSpeed;
    }

    public void BOMBCNT()
    {
        bombLimit++;
        if (bombLimit >= bombMaxLimit)
        {
            bombLimit = bombMaxLimit;
        }
    }

    public void BOMBLENGTH()
    {
        effectMax += 2f;

        if (effectMax >= effectMaxLength)
        {
            effectMax = effectMaxLength;
        }
    }

    public void MAXLENGTH()
    {
        effectMax = effectMaxLength;
    }


    public void DARTS()
    {
        if (playerCtrl.dartsOn == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                GameObject DartsItem = Instantiate(dartsPrefab, playerTr.position, playerTr.rotation);
                DartsItem.GetComponent<Rigidbody>().velocity = playerTr.transform.forward * force;
                dartslimit--;
            }
        }
        if (dartslimit <= 0)
        {
            playerCtrl.dartsOn = false;
            if (playerCtrl.dartsOn == false)
            {
                System.Array.Clear(playerCtrl.item, 0, 1);
            }
        }
    }

    public void SPRING()
    {
        if (playerCtrl.springOn == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                hits = Physics.RaycastAll(playerTr.position, playerTr.forward, 2f);

                int hitL = hits.Length;

                if (hitL > 0 && hitL <= 1)
                {
                    Instantiate(springEffect, playerTr.position, playerTr.rotation);

                    playerTr.transform.Translate(Vector3.forward * 1.5f);
                    springlimit--;
                    itemsAudio.clip = springAudio;
                    itemsAudio.Play();

                    Instantiate(springEffect, playerTr.position, playerTr.rotation);
                }
                else if (hitL > 1 && hitL <= 2)
                {
                    Instantiate(springEffect, playerTr.position, playerTr.rotation);

                    playerTr.transform.Translate(Vector3.forward * 2.5f);
                    springlimit--;
                    itemsAudio.clip = springAudio;
                    itemsAudio.Play();

                    Instantiate(springEffect, playerTr.position, playerTr.rotation);
                }
            }
        }
        if (springlimit <= 0)
        {
            playerCtrl.springOn = false;
            if (playerCtrl.springOn == false)
            {
                System.Array.Clear(playerCtrl.item, 0, 1);
            }
        }
    }

    public void SHIELD()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isShield = true;
            shieldEffect.SetActive(true);

            itemsAudio.clip = shieldAudio;
            itemsAudio.Play();        
        }
    }
    #endregion


    
}