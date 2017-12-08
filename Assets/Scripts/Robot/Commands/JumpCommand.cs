using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : IRobotCommand
{
    private GameObject mRobotObject;
    private Rigidbody2D mRobotBody;
    private RobotController mRobotController;
    private Vector2 mJumpDirectionVector = Vector2.one;
    private float mJumpDirection = 0;
    private float mJumpHeight = 5;
    private bool isOnAir = false;

    private float mChunchuCounter = 1f; // 1s passed cancel the animation

    public float executeAfter()
    {
        return 0f;
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
        return isOnAir;
    }

    public void passParameters(params object[] args)
    {
        
    }

    public void run()
    {
        mRobotController = mRobotObject.GetComponent<RobotController>();
        if (mRobotController.getRobotState() == RobotState.OnFloor)
        {
            isOnAir = true;
            mRobotController.setHashState(GameConsts.kJumpAnimationHash);
            mJumpDirection = mRobotObject.transform.localScale.x;
            mJumpDirectionVector.x *= mJumpDirection;
            mJumpDirectionVector.y *= mJumpHeight;
            mRobotBody = mRobotObject.GetComponent<Rigidbody2D>();
            mRobotBody.AddForce(mJumpDirectionVector, ForceMode2D.Impulse);
        }
    }

    public void update()
    {
        if (isOnAir && mChunchuCounter > 0)
        {
            isOnAir = mRobotController.getRobotState() != RobotState.OnFloor;
            mChunchuCounter -= Time.deltaTime;
        }
        else
        {
            isOnAir = false;
            //mRobotController.setHashState(GameConsts.kIdleAnimationHash);
        }
    }
}
