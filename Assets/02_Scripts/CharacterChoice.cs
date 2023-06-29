using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChoice : MonoBehaviour
{
    public bool isFirst = false;
    public bool isSecond = false;
    public bool isThird = false;

    public int i;

    public ChangeMat changeMat;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void FirstCharacter()
    {
        isFirst = true;

        i = 0;

        SceneManager.LoadScene(1);
    }

    public void SecondCharacter()
    {
        isSecond = true;

        i = 1;

        SceneManager.LoadScene(1);
    }

    public void ThirdCharacter()
    {
        isThird = true;

        i = 2;

        SceneManager.LoadScene(1);
    }
}
