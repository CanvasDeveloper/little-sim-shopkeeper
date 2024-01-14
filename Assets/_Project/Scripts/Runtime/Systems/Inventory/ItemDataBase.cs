using CanvasDEV.Runtime.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = CanvasDevKeys.ScriptablePath + "Items")]
public class ItemDataBase : ScriptableObject
{
    public Sprite itemIcon;

    public string itemName;
    [TextArea]
    public string itemDescription;
    public int cost;

    public bool stackable;
}
