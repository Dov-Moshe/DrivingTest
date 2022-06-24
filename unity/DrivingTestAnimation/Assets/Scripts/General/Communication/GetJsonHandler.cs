using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class GetJsonHandler
    {
        public static T[] getJsonTuple<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
            return wrapper.rules;
        }
        
        [Serializable]
        private class Wrapper<T>
        {
            public T[] rules;
        }
    }