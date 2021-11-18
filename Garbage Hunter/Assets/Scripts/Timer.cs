using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{

    public GameOver gameOver;
    float currentTime = 0f;
    float TimeLimit = 300f;
    public MouseLook mouselook;
    public PlayerMovement player;
    public FallingObjs fobj;

    private int oldscore =0;

    bool intermission = false;
    bool ingame = true;

    [SerializeField] TMPro.TextMeshProUGUI countdownText;

    private void Start()
    {
        TimeLimit = (1+(fobj.getlevel()/(100))) * 300;
        currentTime = TimeLimit;

       // Time.timeScale = 1f;
       // Time.fixedDeltaTime = Time.timeScale;
    }

    private void Update()
    {

        currentTime -= 1 * Time.deltaTime;
        if (ingame == true)
        {
           countdownText.text = currentTime.ToString("0.00");
        } else if (intermission == true)
        {
            countdownText.text = "Intermission: \n" +currentTime.ToString("0.00");
        }

        if (player.getPoint() == oldscore+fobj.getfull())
        {
            currentTime = 0;
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            Debug.Log("oldscore= " + oldscore);
            if (ingame == true)
            {
                if (player.getPoint() >= fobj.getgradeavg())
                {
                    ingame = false;
                    intermission = true;
                    currentTime = 15;
                    oldscore = player.getPoint();
                    GameObject temp = fobj.getlevelgroup();
                    if (temp != null)
                    {
                        Destroy(temp);
                    }
                    player.resetInv();
                }
                else
                {
                    GameOver();
                }
            } else if (intermission == true)
            {
                ingame = true;
                intermission = false;
                fobj.levelplus();
                fobj.LevelStart();
                TimeLimit = (1 + (fobj.getlevel() / (100))) * 300;
                currentTime = TimeLimit;
            }
        }
    }

    public void GameOver()
    {
        gameOver.StartTimeSlow();
        mouselook.CursorUnlock();
    }

}
