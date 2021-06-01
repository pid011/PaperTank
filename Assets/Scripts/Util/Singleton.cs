using UnityEngine;

namespace PaperTank.Util
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T Instance
        {
            get
            {
                if (s_instance == null)
                {
                    var obj = FindObjectOfType<T>();
                    if (obj != null) s_instance = obj;
                }

                return s_instance;
            }
        }
        private static T s_instance;

        protected virtual void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
