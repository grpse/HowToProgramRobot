using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCommand : IRobotCommand
{
    private GameObject mRobotObject;
    private bool mTurned = false;

    public float executeAfter()
    {
        return 0;
    }

    public bool executeBetween()
    {
        return false;
    }

    public void executeOn(GameObject gameObject)
    {
        mRobotObject = gameObject;
    }

    public bool isExecuting()
    {
        return mTurned;
    }

    public void passParameters(params object[] args)
    {

    }

    public void run()
    {
        mTurned = true;
        mRobotObject.transform.localScale = new Vector3(mRobotObject.transform.localScale.x * -1, 1, 1);
        mTurned = false;
    }

    public void update()
    {

    }
}
