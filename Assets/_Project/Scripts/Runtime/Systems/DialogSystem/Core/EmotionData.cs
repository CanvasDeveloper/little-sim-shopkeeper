using CanvasDEV.Runtime.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.DialogSystem.Core
{
    [CreateAssetMenu(fileName = "Emotion Sprite", menuName = CanvasDevKeys.ScriptablePath + "Emotion Data")]
    public class EmotionData : ScriptableObject
    {
        [SerializeField] private List<EmotionSprite> Emotion;

        public Sprite GetSpriteByEmotion(Emotion emotion)
        {
            var foundedData = Emotion.FirstOrDefault(x => x.emotion == emotion);

            if(foundedData == null)
            {
                return null;
            }

            return foundedData.sprite;
        }
    }
}