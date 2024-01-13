using CanvasDEV.Runtime.Core;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteSheet", menuName = CanvasDevKeys.ScriptablePath + "SpriteSheet")]
public class SpriteSheet : ScriptableObject
{
    public List<Sprite> sprites;

    public int GetSpriteIndexOf(Sprite sprite)
    {
        return sprites.IndexOf(sprite);
    }

    public Sprite GetSpriteByIndex(int index)
    {
        return sprites[index];
    }
}
