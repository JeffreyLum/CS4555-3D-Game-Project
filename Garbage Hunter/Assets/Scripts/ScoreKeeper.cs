using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public PlayerMovement player;
    int cur_Score = 0;
    string myText = "0";

    // Start is called before the first frame update
    void Start()
    {
        float interval = 0.1f;
        // keep track of score
       // InvokeRepeating("checkScore", 2, interval);
    }

    // Update is called once per frame
    void Update()
    {
        cur_Score = player.getPoint();
        gameObject.GetComponent<Text>().text = cur_Score + "";
    }


    void checkScore()
    {
        cur_Score = player.getPoint();
        // if curent score is greate than 20: move to next level
        if (cur_Score > 20)   // 20 can be change for your choice
        {
            CancelInvoke();
            invokeWon();
            cur_Score = 0;
            Debug.Log("cancel checking score");
        }
    }

    // to display text and invoke scence load. 
    void invokeWon()
    {
        gameObject.GetComponent<Text>().color = Color.green;
        gameObject.GetComponent<Text>().fontSize = 25;
        //  Giving player 5 seconds to be ready 
        myText = "You Won! Next Challenge in 5 Sec";
        InvokeRepeating("loadNextScene", 5, 5);
        Debug.Log("calling Next scece");

    }

    void loadNextScene()
    {
        Debug.Log("loading Next scece");
        CancelInvoke();
        // load next 
        SceneManager.LoadScene(2);
    }
}
