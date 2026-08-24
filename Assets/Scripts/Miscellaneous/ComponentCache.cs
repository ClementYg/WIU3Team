using UnityEngine;
using System;
using System.Collections.Generic;

public class ComponentCache : MonoBehaviour
{
    readonly Dictionary<Type, Component> cache = new();

    public T Get<T>() where T : Component
    {
        if (!cache.TryGetValue(typeof(T), out Component component))
        {
            component = GetComponent<T>();
            cache[typeof(T)] = component;
        }

        return component as T;
    }
}
