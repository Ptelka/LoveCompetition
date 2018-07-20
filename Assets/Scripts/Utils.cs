
using System;
using UnityEngine;
using Object = System.Object;

public static class Utils
{
    public static GameObject Load(string filename)
    {
        return Load<GameObject>(filename);
    }
    
    public static T Load<T>(string filename) where T : UnityEngine.Object
    {
        T obj = Resources.Load<T>(filename);
        if (!obj)
            throw new NullReferenceException("Couldn't open: " + filename);

        return obj;
    }
}
