using CanvasDEV.Runtime.Core;
using System.Collections.Generic;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.Inventory
{
    [CreateAssetMenu(fileName = "Runtime Inventory", menuName = CanvasDevKeys.ScriptablePath + "Runtime Inventory")]
    public class RuntimeInventory : ScriptableObject
    {
        public List<InventoryItemData> items;
    }
}