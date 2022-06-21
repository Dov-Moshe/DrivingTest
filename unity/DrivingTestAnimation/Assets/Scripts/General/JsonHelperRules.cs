using System;
using UnityEngine;

public static class JsonHelperRules
{
    public static (T[], R, R) FromJson<T, R>(string json)
    {
        Wrapper<T, R> wrapper = JsonUtility.FromJson<Wrapper<T, R>>(json);
        return (wrapper.questions, wrapper.explanation, wrapper.title);
    }

    [Serializable]
    private class Wrapper<T, R>
    {
        public R title;
        public R explanation;
        public T[] questions;
    }
}