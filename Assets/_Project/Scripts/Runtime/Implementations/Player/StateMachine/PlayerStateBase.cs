using CanvasDEV.Runtime.Systems.StateMachine;

public class PlayerStateBase : StateBase
{
    protected PlayerStateMachine machine;

    public override void Setup(StateMachineBase machine)
    {
        this.machine = (PlayerStateMachine)machine;
    }
}
