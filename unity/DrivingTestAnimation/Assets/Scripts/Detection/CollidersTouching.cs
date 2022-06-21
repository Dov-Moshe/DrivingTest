using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CollidersTouching
{
    public static List<Collider> collidersTouching = new List<Collider>();

    
    /*void OnTriggerEnter(Collider other)
    {
        ColliderExist(other) == -1 ? collidersTouching.Add(other) : return;
        if(ColliderExist(other) == null)
        {
            collidersTouching.Add(other);
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        ColliderExist(other) == -1 ? return : collidersTouching.RemoveAt(i);
        if((int i = ColliderExist(other)) != null)
        {
            collidersTouching.RemoveAt(i);
        }
    }*/

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