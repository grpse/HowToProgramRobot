using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelDone : MonoBehaviour {

    private bool mCompleted = false;
    private GameLevelMenu mGameLevelMenu;

    static private GameLevelDone mInstance;

    static public GameLevelDone GetInstance()
    {
        return mInstance;
    }

    public void Awake()
    {
        mInstance = this;
        mGameLevelMenu = GameObject.FindObjectOfType<GameLevelMenu>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameExecutor.GetInstance().stopExecuting();
        mCompleted = true;
        GameData.unlockNextLevel();
        ShowMiddleMenu();
    }
    
    public void ShowMiddleMenu()
    {
        if (mCompleted)
        {
            mGameLevelMenu.ShowWithNextLevelButton();
        }
        else
        {
            mGameLevelMenu.ShowGameOver();
        }
    }
}
