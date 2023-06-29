using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroCharacter : MonoBehaviour
{
    public Animator anim;
    private float cT;
    private float wT = 5f;

    public void OnMouseOver()
    {
        anim.SetBool("Walk", true);
    }
}
