using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CollidersTouching
{
    public static List<Collider> collidersTouching = new List<Collider>();

    public static Nullable<int> ColliderExist(Collider collider)
    {
        for(int i = 0; i < collidersTouching.Count; i++)
        {
            if (GameObject.ReferenceEquals(collider.gameObject, collidersTouching[i].gameObject))
            {
                return i;
            }
        }
        return null;
    }

    public static bool IsTouching(Collider collider)
    {
        if (ColliderExist(collider) >= 0)
        {
            return true;
        }
        return false;
    }
}