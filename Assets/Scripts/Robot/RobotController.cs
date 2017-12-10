using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour {


    [SerializeField]
    private RobotState mState = RobotState.OnFloor;
    private Animator mAnimator;
    private RobotCommandReceiver mCommandReceiver;
    private bool mSetToTheFloor = false;

    public void setHashState(int hashState)
    {
        mAnimator.SetTrigger(hashState);

        if (hashState == GameConsts.kIdleAnimationHash || hashState == GameConsts.kWalkAnimationHash)
        {
            mState = RobotState.OnFloor;
        }
        else if (hashState == GameConsts.kJumpAnimationHash)
        {
            mState = RobotState.Jumping;
        }
    }
    

    // Use this for initialization
    void Start () {
        mAnimator = gameObject.GetComponent<Animator>();
        mCommandReceiver = gameObject.GetComponent<RobotCommandReceiver>();
    }
	
    public RobotState getRobotState()
    {
        return mState;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "floor" && !mSetToTheFloor)
        {
            mState = RobotState.OnFloor;
            setHashState(GameConsts.kIdleAnimationHash);
            mSetToTheFloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "floor" && mSetToTheFloor)
        {
            mState = RobotState.Jumping;
            mSetToTheFloor = false;
        }
    }

    void Turn() {
        IRobotCommand turn = new TurnCommand();
        mCommandReceiver.receiveCommand(turn);        
    }

    void Walk(float time) {
        IRobotCommand walk = new WalkCommand();
        walk.passParameters(time);
        mCommandReceiver.receiveCommand(walk);        
    }

    void Jump() {
        IRobotCommand jump = new JumpCommand();
        mCommandReceiver.receiveCommand(jump);
    }

    void JumpWithDifferentForce(float forcemultiplier) {
        IRobotCommand jump = new JumpCommand();
        jump.passParameters(forcemultiplier);
        mCommandReceiver.receiveCommand(jump);
    }

    void DoubleJump() {
        IRobotCommand djump = new DoubleJumpCommand();
        mCommandReceiver.receiveCommand(djump);
    }

    void Execute() {
        GameExecutor.GetInstance().executeCommands();
    }

    void Pause() {
        GameExecutor.GetInstance().pauseCommands();
    }

    void Stop() {
        GameExecutor.GetInstance().stopExecuting();
    }

    [ContextMenu("Turn")]
    void Test_Turn()
    {
        IRobotCommand turn = new TurnCommand();
        mCommandReceiver.receiveCommand(turn);
    }

    [ContextMenu("Walk_3")]
    void Test_Walk_3()
    {
        IRobotCommand walk = new WalkCommand();
        walk.passParameters(3.0f);
        mCommandReceiver.receiveCommand(walk);
    }

    [ContextMenu("Walk_1")]
    void Test_Walk_1()
    {
        IRobotCommand walk = new WalkCommand();
        walk.passParameters(1.0f);
        mCommandReceiver.receiveCommand(walk);        
    }

    [ContextMenu("Walk_-1")]
    void Test_Walk__1()
    {
        IRobotCommand walk = new WalkCommand();
        walk.passParameters(-1.0f);
        mCommandReceiver.receiveCommand(walk);        
    }

    [ContextMenu("Jump")]
    void Test_Jump()
    {
        IRobotCommand jump = new JumpCommand();
        mCommandReceiver.receiveCommand(jump);
    }

    [ContextMenu("Jump Force")]
    void Test_Jump_Force()
    {
        IRobotCommand jump = new JumpCommand();
        jump.passParameters(2.0f);
        mCommandReceiver.receiveCommand(jump);
    }

    [ContextMenu("Double Jump")]
    void Test_DoubleJump()
    {
        IRobotCommand djump = new DoubleJumpCommand();
        mCommandReceiver.receiveCommand(djump);
    }

    [ContextMenu("Test Jump + DoubleJump")]
    void Test_Jump_p_DoubleJump() {
        mCommandReceiver.receiveCommand(new JumpCommand());
        mCommandReceiver.receiveCommand(new DoubleJumpCommand());
    }

    [ContextMenu("Execute Command Queue")]
    void Test_ExecuteCommandQueue()
    {
        GameExecutor.GetInstance().executeCommands();
    }
}
