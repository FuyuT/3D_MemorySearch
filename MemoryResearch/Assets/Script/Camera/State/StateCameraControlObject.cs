using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<CameraManager>;

public class StateCameraControlObject : State
{
    protected override void OnEnter(State prevState)
    {
        //オブジェクトのマテリアル変更
        Owner.colorChange.ChangeAfterMaterial();
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        if (Input.GetKeyDown("v"))
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.Floor);
        }
    }

    protected override void OnExit(State nextState)
    {
        //オブジェクトのマテリアルを戻す
        Owner.colorChange.ChangeBeforeMaterial();
        Owner.MoveObjCamScript.ChangFlg = false;
    }
}
