using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static bool IsEqualTo(this Color me, Color other)
    {
        return Mathf.Approximately(me.r,other.r) && Mathf.Approximately(me.g, other.g) && Mathf.Approximately(me.b, other.b) && Mathf.Approximately(me.a, other.a);
    }
}
