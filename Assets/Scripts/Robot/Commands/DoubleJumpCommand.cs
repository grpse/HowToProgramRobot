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
        return mAnimationDuration > 0;
    }

    public void passParameters(params object[] args)
    {
        
    }

    public void run()
    {
        Debug.Log("Going to perform double jumpt...");
        mImpulseVector = new Vector3(mRobotObject.transform.localScale.x * 0.01f, 0.65f, 0f);
        mRobotBody.AddForce(mImpulseVector, ForceMode2D.Impulse);
        mRobotController.setHashState(GameConsts.kDoubleJumpAnimationHash);
    }

    public void update()
    {
        mAnimationDuration -= Time.deltaTime;

        if (mAnimationDuration <= 0f)
        {
            mRobotController.setHashState(GameConsts.kJumpAnimationHash);
        }
    }
}
