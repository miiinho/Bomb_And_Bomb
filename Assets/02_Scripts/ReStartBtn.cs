using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReStartBtn : MonoBehaviour
{
    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }
}
