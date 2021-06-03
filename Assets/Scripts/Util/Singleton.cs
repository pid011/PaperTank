using UnityEngine;

namespace PaperTank.Util
{
    public class Singleton<T> where T : MonoBehaviour
    {
        protected Singleton() { }

        private static T singletonInstance { get; set; }

        protected static T instance
        {
            get
            {
                if (singletonInstance == null)
                {
                    var obj = Object.FindObjectOfType<T>();
                    if (obj != null) singletonInstance = obj;
                }

                return singletonInstance;
            }
        }
    }
}
