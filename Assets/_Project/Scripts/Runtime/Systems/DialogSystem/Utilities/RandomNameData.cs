using CanvasDEV.Runtime.Core;
using System.Collections.Generic;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.DialogSystem.Utilities
{
    [CreateAssetMenu(fileName = "Random names", menuName = CanvasDevKeys.ScriptablePath + "Dialog/Random Names")]
    public class RandomNameData : ScriptableObject
    {
        [SerializeField] private List<string> names = new();

        public string GenerateName()
        {
            return names[Random.Range(0, names.Count)];
        }
    }
}