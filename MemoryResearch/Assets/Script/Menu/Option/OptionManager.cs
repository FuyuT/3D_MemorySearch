using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [Header("ƒ{ƒ^ƒ“—Þ")]
    [SerializeField] GameObject AutoSaveButton;
    [SerializeField] GameObject BGMButton;
    [SerializeField] GameObject SEButton;
    [SerializeField] GameObject AimXButton;
    [SerializeField] GameObject AimYButton;
    [SerializeField] GameObject ReturnButton;
    [SerializeField] GameObject ResetButton;

    public enum ButtonType
    {
        AutoSave,
        BGM,
        SE,
        AimX,
        AimY
    }

    StateMachine<OptionManager> stateMachine;
    public int GetCurrentButtonType() { return stateMachine.currentStateKey; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StateMachineInit()
    {
        //stateMachine = new StateMachine<OpenOption>(this);

        stateMachine.AddAnyTransition<StateBGMButton>((int)ButtonType.BGM);
        //stateMachine.AddAnyTransition<StateCameraFPS>((int)CameraType.FPS);
        //stateMachine.AddAnyTransition<StateCameraControlObject>((int)CameraType.Controller);

        //stateMachine.AddAnyTransition<StateCameraFloor>((int)CameraType.Floor);

        //stateMachine.Start(stateMachine.GetOrAddState<StateCameraTPS>());
        //stateMachine.currentStateKey = (int)CameraType.TPS;
    }
}
