using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCommand : IRobotCommand {

    private Vector3 mWalkVelocityVector = Vector3.zero;
    private int mWalkDirection = 0;
    private float mTimeWalkingInSeconds = 0;
    private float mSpeed = 1f;
    private bool mIsWalking = false;
    private GameObject mRobotObject;
    private Animator mAnimator;
    private RobotController mRobotController;

    public void run()
    {
        mRobotController = mRobotObject.GetComponent<RobotController>();
        mAnimator = mRobotObject.GetComponent<Animator>();
        mIsWalking = true;
        mWalkDirection = mRobotObject.transform.localScale.x < 0 ? -1 : 1;
        mWalkVelocityVector.x = mSpeed * mWalkDirection;
        mRobotController.setHashState(GameConsts.kWalkAnimationHash);
    }

    public void passParameters(params object[] args)
    {
        mTimeWalkingInSeconds = (float)args[0];
    }

    public void executeOn(GameObject gameObject)
    {
        mRobotObject = gameObject;
    }

    public bool isExecuting()
    {
        return mIsWalking;
    }

    public void update()
    {
        
        if (mTimeWalkingInSeconds > 0)
        {
            Vector3 nextStep = mWalkVelocityVector * Time.deltaTime;
            Debug.Log("Last Position: " + mRobotObject.transform.localPosition);
            mRobotObject.transform.localPosition += nextStep;
            Debug.Log("Next Position: " + mRobotObject.transform.localPosition);
            Debug.Log("Position Step: " + nextStep);
        }
        else
        {
            mIsWalking = false;
        }

        mTimeWalkingInSeconds -= Time.deltaTime;        
    }

    public bool executeBetween()
    {
        return false;
    }

    public float executeAfter()
    {
        return 0;
    }
}
