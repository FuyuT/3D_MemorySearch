using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.State<CameraManager>;

public class StateCameraControlObject : State
{
    protected override void OnEnter(State prevState)
    {
        //オブジェクトのマテリアル変更
       // Owner.colorChange.ChangeAfterMaterial();
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //if (Input.GetKeyDown("r"))
        //{
        //    stateMachine.Dispatch((int)CameraManager.CameraType.TPS);
        //}
    }

    protected override void OnExit(State nextState)
    {
        //オブジェクトのマテリアルを戻す
       // Owner.colorChange.ChangeBeforeMaterial();
    }
}
