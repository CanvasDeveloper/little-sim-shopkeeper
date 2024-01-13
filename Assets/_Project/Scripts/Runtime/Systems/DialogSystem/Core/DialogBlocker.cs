using CanvasDEV.Runtime.Core.Interfaces;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.DialogSystem.Core
{
    public class DialogBlocker : MonoBehaviour, IBlocker
    {
        public IBlockable[] blockables { get; set; }
    }
}