using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (!_instance)         //if no instance yet, find existing gameobject in scene with that component
            {
                _instance = (T)FindObjectOfType(typeof(T));
            }
            
            if (!_instance)         //if no instance still after checking scene, set null
            {
                _instance = null;
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)      //set self as T if no instance
        {
            _instance = this as T;
        }

        if (_instance != null)      //if instance exists, check if self is instance
        {
            if (_instance != this as T)     //if self is not instance, destroy self
            {
                Destroy(this.gameObject);
            }
        }
    }
}
