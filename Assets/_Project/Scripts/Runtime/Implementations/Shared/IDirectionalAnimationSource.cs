public interface IDirectionalAnimationSource
{
    public float XDirection { get; set; }
    public float YDirection { get; set; }
    bool OnMovement { get; set; }
    bool IsRunning { get; set; }
}
