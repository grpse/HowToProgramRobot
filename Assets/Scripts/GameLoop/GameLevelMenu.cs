using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLevelMenu : MonoBehaviour {

    private RectTransform mRectTransform;

    [SerializeField]
    private Text mMainText;
    [SerializeField]
    private Button mRepeatLevelButton;
    [SerializeField]
    private Button mLevelSelectButton;
    [SerializeField]
    private Button mNextLevelButton;

    public void Awake()
    {
        mRectTransform = (RectTransform)transform;
        mRepeatLevelButton.onClick.AddListener(this.LoadLevelAgain);
        mLevelSelectButton.onClick.AddListener(this.LoadLevelSelectMenu);
        mNextLevelButton.onClick.AddListener(this.LoadNextLevel);
    }

    public void HideMiddleMenu()
    {
        mRectTransform.localPosition = new Vector2(0, -300f);
    }

    public void ShowWithNextLevelButton()
    {
        mMainText.text = "Congrats!! :D";
        mNextLevelButton.interactable = true;
        AnimateToShow();
    }

    public void ShowGameOver()
    {
        mMainText.text = "Game Over :/";
        mNextLevelButton.interactable = false;
        AnimateToShow();
    }

    private void LoadLevelAgain()
    {
        GameData.reloadCurrentLevel();
    }

    private void LoadLevelSelectMenu()
    {
        GameData.loadLevelSelectMenu();
    }

    private void LoadNextLevel()
    {
        GameData.loadNextLevel();
    }

    private void AnimateToShow()
    {
        StartCoroutine(InterpolateToShow());
    }

    private IEnumerator InterpolateToShow()
    {
        float finalY = 0;
        Vector3 velocity = new Vector3(0, 1f);

        while(mRectTransform.localPosition.y < finalY)
        {
            mRectTransform.localPosition = Vector3.SmoothDamp(mRectTransform.localPosition, Vector3.zero, ref velocity, Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.05f);
        }

        mRectTransform.localPosition = Vector3.zero;

        yield return null;
    }

    [ContextMenu("Show Game Over")]
    void Test_ShowGameOver()
    {
        ShowGameOver();
    }

    [ContextMenu("Show ConGrats")]
    void Test_ShowCongrats()
    {
        ShowWithNextLevelButton();
    }

}
