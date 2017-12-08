using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour {

    private int mLastLevelUnlocked = 0;
    private int mLevelLoaded = 0;

    static private GameData mInstance;
    static private GameData Instance {
        get {
            if (mInstance == null)
            {
                new GameObject("GameData").AddComponent<GameData>();
            }

            return mInstance;
        }
    }

    static public int getLevelUnlocked()
    {
        return Instance.mLastLevelUnlocked;
    }

    static public void unlockNextLevel()
    {
        if (Instance.mLastLevelUnlocked == Instance.mLevelLoaded)
            Instance.mLastLevelUnlocked = Instance.mLevelLoaded + 1;
    }

    static public void loadLevel(int level)
    {
        Instance.mLevelLoaded = level;
        SceneManager.LoadScene("Level" + level.ToString());
    }

    static public void reloadCurrentLevel()
    {
        SceneManager.LoadScene("Level" + Instance.mLevelLoaded.ToString());
    }

    static public void loadNextLevel()
    {
        loadLevel(Instance.mLevelLoaded + 1);
    }

    static public void loadLevelSelectMenu()
    {
        SceneManager.LoadScene(GameConsts.LevelSelectorSceneName);
    }

    private void Awake()
    {
        mInstance = this;
        GameObject.DontDestroyOnLoad(this);
    }
}
