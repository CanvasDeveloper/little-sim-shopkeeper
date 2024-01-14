using CanvasDEV.Runtime.Core;
using UnityEngine;

public enum EquipType
{
    NONE,
    Head,
    Body,
    Under,
    Clothes
}

[CreateAssetMenu(fileName = "EquipabbleItem", menuName = CanvasDevKeys.ScriptablePath + "Equippable Item")]
public class EquippableItemData : ItemDataBase
{
    public EquipType equipType;

    public AnimatorOverrideController overrideControler;
}
