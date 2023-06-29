using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject popUp;
    public GameObject characterChoice;

    public void HowToPlay()
    {
        popUp.SetActive(true);
    }

    public void CharacterChoice()
    {
        characterChoice.SetActive(true);
    }
}
