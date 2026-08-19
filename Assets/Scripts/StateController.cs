using UnityEngine;
using System;
using System.Collections.Generic;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;
    private readonly Dictionary<Type, Component> cache = new();

    public T GetCached<T>() where T : Component
    {
        if (!cache.TryGetValue(typeof(T), out Component component))
        {
            component = GetComponent<T>();
            cache[typeof(T)] = component;
        }
        return component as T;
    }    

    private void Start()
    {
        currentState.Init(this);
    }

    private void Update()
    {
        //Debug.Log($"Current State: {currentState}");
        currentState.Execute(this);
        currentState.CheckTransitions(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState == null)
        {
            Debug.LogError($"{name}: attempted to transition to a null State");
        }
        
        if (nextState != remainState)
        {
            currentState.End(this);
            currentState = nextState;
            currentState.Init(this);
        }
    }
}
