using CanvasDEV.Runtime.Core;
using System;
using UnityEngine;

[Serializable]
public class EquipableOverrider
{
    public EquippableItemData equipItem;
}

[CreateAssetMenu(fileName = "Equipabble Data", menuName = CanvasDevKeys.ScriptablePath + "Equippable Settings")]
public class EquipableData : ScriptableObject
{
    public EquipableOverrider[] OverrideControllerArray;
}
