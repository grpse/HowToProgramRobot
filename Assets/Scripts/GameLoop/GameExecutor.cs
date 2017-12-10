using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExecutor : MonoBehaviour {

    static private GameExecutor mInstance;

    private Queue<IRobotCommand> mCommandQueue;
    private Coroutine mExecutionRoutine;
    private bool mIsExecutingGame = false;
    private RobotController mRobotController;

    static public GameExecutor GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = (new GameObject("GameExecutor").AddComponent<GameExecutor>());
        }

        return mInstance;
    }

    public void Awake()
    {
        mRobotController = GameObject.FindObjectOfType<RobotController>();
        mCommandQueue = new Queue<IRobotCommand>();
        mInstance = this;
    }

    public void enqueueCommand(IRobotCommand command)
    {        
        mCommandQueue.Enqueue(command);
    }

    public void executeCommands()
    {
        setExecutionState(true);
        mExecutionRoutine = StartCoroutine(ExecuteCommandsCorountine());
    }

    public void pauseCommands()
    {
        if (mExecutionRoutine != null)
        {
            setExecutionState(false);
            StopCoroutine(mExecutionRoutine);
        }
    }

    public void stopExecuting()
    {
        if (mExecutionRoutine != null)
        {
            setExecutionState(false);
            StopCoroutine(mExecutionRoutine);
            mCommandQueue.Clear();
        } 
    }

    public bool isExecutingGame()
    {
        return mIsExecutingGame;
    }

    private void setExecutionState(bool executionState)
    {
        mIsExecutingGame = executionState;
    }

    private IEnumerator ExecuteCommandsCorountine()
    {

        while(mCommandQueue.Count > 0)
        {
            IRobotCommand command = mCommandQueue.Dequeue();
            IRobotCommand invades = mCommandQueue.Count >= 1 ? mCommandQueue.Peek() : new NullCommand();
            float timeToExecuteInvades = 0;
            bool betweenCommandExecuted = false;
            command.run();
            if (invades.executeBetween())
            {
                timeToExecuteInvades = invades.executeAfter();
                invades = mCommandQueue.Dequeue();
                betweenCommandExecuted = false;
            }                

            while (command.isExecuting())
            {
                if (invades.executeBetween())
                {
                    if (timeToExecuteInvades <= 0 && !betweenCommandExecuted)
                    {
                        invades.run();
                        betweenCommandExecuted = true;
                    }

                    timeToExecuteInvades -= Time.deltaTime;
                }

                command.update();
                yield return null;
            }

            while(invades.isExecuting()) {
                invades.update();

                yield return null;
            }

            yield return null;
        }

        Debug.Log("Done executing commands...");
        // TODO: Verify game state if level finished or not
        mIsExecutingGame = false;

        // when all executed go to idle
        mRobotController.setHashState(GameConsts.kIdleAnimationHash);

        GameLevelDone.GetInstance().ShowMiddleMenu();
        yield return null;
    }
}
