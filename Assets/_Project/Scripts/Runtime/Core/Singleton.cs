using UnityEngine;

namespace CanvasDEV.Runtime.Core
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool dontDestroy;

        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                string typename = typeof(T).Name;
                Debug.LogWarning($"More that one instance of {typename} found.");
                Destroy(gameObject);
                return;
            }

            Instance = this as T;

            if(dontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}