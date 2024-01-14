using System;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.Inventory
{
    [Serializable]
    public class ActiveEquipmentLocation
    {
        public EquipType type;
        public Animator animator;
    }
}