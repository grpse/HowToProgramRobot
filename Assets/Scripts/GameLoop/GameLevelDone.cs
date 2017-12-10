using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class GameLevelDone : MonoBehaviour {

    private bool mCompleted = false;
    private GameLevelMenu mGameLevelMenu;

    static private GameLevelDone mInstance;

    static public GameLevelDone GetInstance()
    {
        return mInstance;
    }

    [DllImport("__Internal")]
    private static extern void ExecutionFinished(bool completed);

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
            ExecutionFinished(mCompleted);
        }
        else
        {
            mGameLevelMenu.ShowGameOver();
            ExecutionFinished(mCompleted);
        }
    }
}
