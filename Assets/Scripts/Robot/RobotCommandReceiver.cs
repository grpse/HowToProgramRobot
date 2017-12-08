using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCommandReceiver : MonoBehaviour {

    private GameExecutor mGameExecutor;

    public void Awake()
    {
        mGameExecutor = GameExecutor.GetInstance();
    }

    public void receiveCommand(IRobotCommand command)
    {
        command.executeOn(gameObject);
        mGameExecutor.enqueueCommand(command);
    }
}
