using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelController : MonoBehaviour {


    private void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            SelectLevel selectLevel = child.gameObject.GetComponent<SelectLevel>();
            selectLevel.setLevel(i);
            selectLevel.setLastLevelUnlocked(GameData.getLevelUnlocked());
        }
    }
}
