using UnityEngine;

namespace PaperTank.Util
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T instance
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
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
