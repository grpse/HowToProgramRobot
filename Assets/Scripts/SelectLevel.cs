using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour {
    
    private int mLevel = 0;
    private int mLastLevelUnlocked = 0;

    [SerializeField]
    private Text mLevelText;
    [SerializeField]
    private Button mLevelLoadButton;
    [SerializeField]
    private Image mLockImage;

    private void Awake()
    {

    }

    public void setLevel(int level)
    {
        mLevel = level;
        mLevelText.text = level.ToString();
    }

    public void setLastLevelUnlocked(int lastLevelUnlocked)
    {
        mLastLevelUnlocked = lastLevelUnlocked;

        if (mLastLevelUnlocked >= mLevel)
        {
            removeLockInFrontOfTheLevelButton();
            setupButtonToUnlockLevel();
        }
    }

    private void removeLockInFrontOfTheLevelButton()
    {
        mLockImage.gameObject.SetActive(false);
    }

    private void setupButtonToUnlockLevel()
    {
        mLevelLoadButton.onClick.AddListener(this.loadLevel);
    }

    private void loadLevel()
    {
        GameData.loadLevel(mLevel);
    }
}