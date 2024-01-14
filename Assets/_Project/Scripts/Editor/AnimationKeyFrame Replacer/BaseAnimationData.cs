using CanvasDEV.Runtime.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "Base Animation SO", menuName = CanvasDevKeys.ScriptablePath + "Base Animation SO")]
public class BaseAnimationData : ScriptableObject
{
    public SpriteSheet spriteSheet;

    public AnimationClip[] clips;
    public RuntimeAnimatorController animatorController;
}
