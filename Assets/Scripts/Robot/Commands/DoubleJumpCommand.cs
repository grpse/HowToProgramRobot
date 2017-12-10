using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCommand : IRobotCommand
{
    private bool mCanDoDoubleJump = false;
    private Animator mRobotAnimator;
    private GameObject mRobotObject;
    private float mAnimationDuration = 1f;
    private float mChunchuCounter = 2f;
    private bool mIsOnAir = false;
    private Vector3 mImpulseVector;
    private Rigidbody2D mRobotBody;
    private RobotController mRobotController;

    public float executeAfter()
    {
        return 0.7f;
    }

    public bool executeBetween()
    {
        return true;
    }

    public void executeOn(GameObject gameObject)
    {
        mRobotObject = gameObject;
        mRobotAnimator = mRobotObject.GetComponent<Animator>();
        mCanDoDoubleJump = mRobotAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash == GameConsts.kJumpAnimationHash;
        mRobotBody = mRobotObject.GetComponent<Rigidbody2D>();
        mRobotController = mRobotObject.GetComponent<RobotController>();
    }

    public bool isExecuting()
    {
        return mAnimationDuration > 0 && mIsOnAir;
    }

    public void passParameters(params object[] args)
    {
        
    }

    public void run()
    {
        float direction = mRobotObject.transform.localScale.x;
        mIsOnAir = true;
        mImpulseVector = new Vector3(direction, 10f, 0f);
        mRobotBody.AddForce(mImpulseVector, ForceMode2D.Impulse);
        mRobotController.setHashState(GameConsts.kDoubleJumpAnimationHash);
    }

    public void update()
    {
        mAnimationDuration -= Time.deltaTime;

        if (mAnimationDuration <= 0)
        {
            mRobotController.setHashState(GameConsts.kJumpAnimationHash);
        }

        if (mIsOnAir && mChunchuCounter > 0)
        {
            mIsOnAir = mRobotController.getRobotState() != RobotState.OnFloor;
            mChunchuCounter -= Time.deltaTime;
        }
        else
        {
            mIsOnAir = false;
            //mRobotController.setHashState(GameConsts.kIdleAnimationHash);
        }
    }
}
