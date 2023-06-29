using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    public PlayerCtrl playerCtrl;
    public GameObject gameOver;
    public GameObject win;
    public GameObject lose;
    public GameObject draw;

    public bool end = false;
    public bool isWin = false;
    private bool isLose = false;
    private bool isDraw = false;

    public Timer timer;


    void Update()
    {
        if(playerCtrl.isDie == true)
        {         
            lose.SetActive(true);
            gameOver.SetActive(true);

            end = true;
        }

        if(timer.time == 0)
        {
            draw.SetActive(true);
            gameOver.SetActive(true);

            end = true;
        }

        if(isWin == true)
        {
            win.SetActive(true);
            gameOver.SetActive(true);

            end = true;
        }

        StartCoroutine(TimeStop());
    }

    IEnumerator TimeStop()
    {
        if(end == true)
        {
            yield return new WaitForSeconds(0.6f);

            Time.timeScale = 0;
        }   
    }
}
