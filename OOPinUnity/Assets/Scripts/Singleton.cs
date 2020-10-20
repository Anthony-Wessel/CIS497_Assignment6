﻿using System.Collections;
using System.Collections.Generic;
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
