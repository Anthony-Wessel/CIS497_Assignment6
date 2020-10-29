/*
 * Anthony Wessel
 * Assignment 6
 * Used to create singleton objects which only exist once in a scene
 */

using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    static T instance;
    public static T Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("[Singleton] trying to instantiate second instance of singleton class");
        }
        else
        {
            instance = (T)this;
        }
    }

    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
