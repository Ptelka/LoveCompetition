
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

    public static float MaxAbs(float a, float b)
    {
        float abs_a = Mathf.Abs(a);
        float abs_b = Mathf.Abs(b);
        float max = Mathf.Max(abs_a, abs_b);
        return Mathf.Approximately(max, abs_a) ? a : b;
    }
}
