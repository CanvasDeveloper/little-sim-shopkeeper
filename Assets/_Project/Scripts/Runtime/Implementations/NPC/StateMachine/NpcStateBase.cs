using CanvasDEV.Runtime.Systems.StateMachine;

public class NpcStateBase : StateBase
{
    protected NpcStateMachine machine;

    public override void Setup(StateMachineBase machine)
    {
        this.machine = (NpcStateMachine)machine;
    }
}
