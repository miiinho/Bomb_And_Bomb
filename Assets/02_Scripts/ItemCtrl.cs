using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    public GameObject bombCnt;
    public GameObject bombLength;
    public GameObject maxLength;
    public GameObject booster;
    public GameObject maxBooster;
    public GameObject darts;
    public GameObject shield;
    public GameObject spring;


    void Start()
    {
        int items = Random.Range(1, 101);

        if (items == 1)
        {
            Instantiate(darts, transform.position, transform.rotation);
        }

        else if (items == 2)
        {
            Instantiate(spring, transform.position, transform.rotation);

        }

        else if (items == 3)
        {
            Instantiate(shield, transform.position, transform.rotation);

        }

        else if (items == 4 || items == 5)
        {
            Instantiate(maxBooster, transform.position, transform.rotation);

        }

        else if (items == 6 || items == 7)
        {
            Instantiate(bombCnt, transform.position, transform.rotation);

        }

        else if (items == 8 || items == 9)
        {
            Instantiate(bombLength, transform.position, transform.rotation);

        }

        else if (items == 10 || items == 11)
        {
            Instantiate(maxLength, transform.position, transform.rotation);

        }

        else if(items == 12 || items == 13)
        {
            Instantiate(booster, transform.position, transform.rotation);
        }

        else
        {

        }
    }
}

