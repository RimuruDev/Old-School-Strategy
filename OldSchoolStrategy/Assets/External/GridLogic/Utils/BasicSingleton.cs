using UnityEngine;

namespace RimuruDev.External.GridLogic.Utils
{
    public abstract class BasicSingleton<T> : MonoBehaviour where T : BasicSingleton<T>
    {
        public static T instance;

        protected virtual void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }

            instance = (T)this;
        }
    }
}