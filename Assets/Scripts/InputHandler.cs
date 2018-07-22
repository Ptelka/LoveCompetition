using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputHandler {
    public enum Type
    {
        USE,
        PUNCH, 
        NONE
    }

    public static int GetJoyId(int pid)
    {
        string[] names = Input.GetJoystickNames();
        List<int> joyids = new List<int>();
        
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i] != null)
            {
                joyids.Add(i + 1);
            }
        }

        if (pid == 1 && joyids.Count > 0)
            return 1;

        if (pid == 2 && joyids.Count > 1)
            return 2;

        return pid;
    }

    public static bool GetInput(Type t, int pid)
    {
        switch (t)
        {
            case Type.USE:
                return Input.GetButtonDown("SQR" + pid);
            case Type.PUNCH:
                return Input.GetButtonDown("BTN" + pid);
        }
        
        return false;
    }

    public static float GetVertical(int pid)
    {
        return Input.GetAxis("LV" + pid);
    }

    public static float GetHorizontal(int pid)
    {
        return Input.GetAxis("LH" + pid);
    }
}
