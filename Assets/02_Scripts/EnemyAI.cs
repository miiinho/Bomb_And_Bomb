using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    private float bombLimit = 1f;
    private float bombMaxLimit = 6f;
    private float bombCnt = 0;
    private float effectMax = 1f;
    private float effectMaxLength = 7f;
    private float curTime;
    private float chaseTime = 15f;
    public float detectRadius = 1f;
    public float minDis = 2f;
    public float maxDis = 5f;

    public float attackTime = 1f;
    public float viewDist = 30f;
    public float attackRange = 1f;
    public float itemFindDis = 5f;
    private float curT =0;
    private float gameStart = 40f;

    public GameManager gameManager;
    public GameObject bomb;
    public GameObject player;
    public GameObject targetItem;
    public Transform playerTr;
    public Animator enemyAnim;
    public LayerMask bombLayer;
    public LayerMask itemLayer;

    public bool isDie = false;
    public bool isChase = false;
    public bool isAttack = false;

    public Vector3 destination;
    public Vector3 avoidDirection;
    private NavMeshAgent nav;
    public PlayerCtrl playerCtrl;

    public AudioSource bombaudio;

    void Start()
    {
        playerTr = player.GetComponent<Transform>();
        playerCtrl = player.GetComponent<PlayerCtrl>();
        enemyAnim = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();

        isChase = true;
        isAttack = true;

    }

    void Update()
    {
        curT += Time.deltaTime;

        if(curT <= gameStart)
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Bomb"), true);
        }

        else if(curT > gameStart)
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Bomb"), false);
        }

        if (playerCtrl.isDie)
        {
            moveSpeed = 0;

            isChase = false;
            isAttack = false;
            
        }

        float distance = Vector3.Distance(gameObject.transform.position, playerTr.position);

        if (!isDie)
        {
            Chase();

            BombSearch();

            if (distance <= attackRange)
            {
                Attack();
            }
        }

        else if (isDie)
        {
            StartCoroutine(EnemyDie());
        }

        if (!isChase)
        {
            curTime += Time.deltaTime;
            
            if(curTime >= chaseTime)
            {
                curTime = 0;
                isChase = true;
            }
        }
    }

    void BombSearch()
    {
        Vector3[] rayDirections = new Vector3[]
        {
            transform.forward,
            transform.right,
            -transform.forward,
            -transform.right
        };

        foreach(Vector3 direction in rayDirections)
        {
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, detectRadius, bombLayer))
            {
                isChase = false;               

                float distance = Vector3.Distance(transform.position, hit.point);

                if(distance < minDis)
                {
                    Vector3 disToTarget = transform.position - hit.point;
                    Vector3 newPos = transform.position + disToTarget.normalized * maxDis;

                    nav.SetDestination(newPos);

                    StartCoroutine(Stop());
                }
            }
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(2f);

        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Idle", true);

        nav.ResetPath();
        yield return new WaitForSeconds(3f);

        isChase = true;

        yield return new WaitForSeconds(2f);

        isAttack = true;
    }

    void Chase()
    {
        if(isChase == true)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitData;

            enemyAnim.SetBool("Idle", false);
            enemyAnim.SetBool("Walk", true);

            destination = playerTr.position;

            nav.SetDestination(destination);

            if (Physics.Raycast(ray, out hitData, 0.5f))
            {
                if (hitData.transform.tag == "BOX")
                {                  
                    if (isAttack == true)
                    {
                        Attack();

                        isAttack = false;
                    }
                }
            }
        }   
    }

    void Attack()
    {
        if (bombCnt < bombLimit && bombCnt >= 0)
        {
            Vector3 bombPos = new Vector3(transform.position.x, 0, transform.position.z);

            GameObject bombPrefab = Instantiate(bomb, bombPos, Quaternion.Euler(0, 0, 0));

            ++bombCnt;

            StartCoroutine(BombCnt());
        }
    }

    IEnumerator EnemyDie()
    {
        enemyAnim.SetBool("Idle", false);
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Die", true);

        isChase = false;
        isAttack = false;

        moveSpeed = 0;

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    IEnumerator BombCnt()
    {
        yield return new WaitForSeconds(3f);

        bombCnt--;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "BOOSTER":           
                Destroy(other.gameObject);
                break;

            case "MAXBOOSTER":              
                Destroy(other.gameObject);
                break;

            case "BOMBCNT":
                bombLimit++;
                if (bombLimit >= bombMaxLimit)
                {
                    bombLimit = bombMaxLimit;
                }
                Destroy(other.gameObject);
                break;

            case "BOMBLENGTH":
                effectMax += 2f;

                if (effectMax >= effectMaxLength)
                {
                    effectMax = effectMaxLength;
                }
                Destroy(other.gameObject);
                break;

            case "MAXLENGTH":
                effectMax = effectMaxLength;
                Destroy(other.gameObject);
                break;

            case "DARTS":
                Destroy(other.gameObject);
                break;

            case "SPRING":
                Destroy(other.gameObject);
                break;

            case "SHIELD":
                Destroy(other.gameObject);
                break;

            case "EFFECT":
                isDie = true;
                break;
        }
    }

}
