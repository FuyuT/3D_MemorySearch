using MyUtil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.State<CameraManager>;

public class StateCameraFPS : State
{
    protected override void OnEnter(State prevState)
    {
        //カーソルロック
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //spaceを押していなければ終了
        if (!Input.GetKeyDown("space"))
        {
            return;
        }

        switch (stateMachine.beforeStateKey)
        {
            case (int)CameraManager.CameraType.TPS:
                stateMachine.Dispatch((int)CameraManager.CameraType.TPS);
                break;
            //case (int)CameraManager.CameraType.Floor:
            //    stateMachine.Dispatch((int)CameraManager.CameraType.Floor);
            //    break;
        }
    }

    protected override void OnExit(State<CameraManager> nextState)
    {
        //カーソルロック
        Cursor.lockState = CursorLockMode.None;
    }
}
