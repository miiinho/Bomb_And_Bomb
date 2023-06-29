using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFind : MonoBehaviour
{
    public List<GameObject> FoundObjects;
    public GameObject map;

    public Transform bomb;

    public float shortDis;

    void Start()
    {
        bomb = GetComponent<Transform>();
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("MAP"));
        shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);

        map = FoundObjects[0];

        foreach(GameObject found in FoundObjects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if(distance < shortDis)
            {
                map = found;
                shortDis = distance;
            }
        }

        bomb.transform.position = new Vector3(map.transform.position.x, 0, map.transform.position.z);

    }
}
