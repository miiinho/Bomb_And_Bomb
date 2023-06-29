using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset = new Vector3(0, 3f, -2f);

    void Update()
    {
        transform.position = player.transform.position + offset;  
    }
}
