using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    public CharacterChoice cc;

    public Material[] mat = new Material[3];

    void Start()
    {
        cc = GameObject.Find("CharacterChoice").GetComponent<CharacterChoice>();

        int n = cc.i;

        gameObject.GetComponent<SkinnedMeshRenderer>().material = mat[n];
    }

}
