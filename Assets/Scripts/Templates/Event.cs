using UnityEngine;
using System;

/// <summary>
/// Generic Event Channel that takes in 0 arguments
/// </summary>
public class Event : ScriptableObject
{
    private Action Listeners;

    /// <summary>
    /// Registers function argument to trigger upon event being raised
    /// </summary>
    public void Subscribe(Action function)
    {
        Listeners += function;
    }

    /// <summary>
    /// Unregisters function argument to no longer trigger upon event being raised
    /// </summary>
    public void Unsubscribe(Action function)
    {
        Listeners -= function;
    }

    /// <summary>
    /// Raises the event, invoking all subscribed listeners.
    /// </summary>
    public void Raise()
    {
        Listeners?.Invoke();
    }

    private void OnDisable()
    {
        Listeners = null; // Safety net in case Unsubscribe() is not called
    }
}

/// <summary>
/// Generic Event Channel that takes in 1 argument
/// </summary>
public class Event<T> : ScriptableObject
{
    private Action<T> Listeners;

    /// <inheritdoc cref="Event.Subscribe(Action)"/>
    public void Subscribe(Action<T> function)
    {
        Listeners += function;
    }

    /// <inheritdoc cref="Event.Unsubscribe(Action)"/>
    public void Unsubscribe(Action<T> function)
    {
        Listeners -= function;
    }

    /// <inheritdoc cref="Event.Raise"/>
    public void Raise(T arg)
    {
        Listeners?.Invoke(arg);
    }

    private void OnDisable()
    {
        Listeners = null; // Safety net in case Unsubscribe() is not called
    }
}

/// <summary>
/// Generic Event Channel that takes in 2 arguments
/// </summary>
public class Event<T1, T2> : ScriptableObject
{
    private Action<T1, T2> Listeners;

    /// <inheritdoc cref="Event.Subscribe(Action)"/>
    public void Subscribe(Action<T1, T2> function)
    {
        Listeners += function; // Literally adds a function that's triggered upon event being raised
    }

    /// <inheritdoc cref="Event.Unsubscribe(Action)"/>
    public void Unsubscribe(Action<T1, T2> function)
    {
        Listeners -= function; // Removes function to be triggered upon event being raised
    }

    /// <inheritdoc cref="Event.Raise"/>
    public void Raise(T1 arg1, T2 arg2)
    {
        Listeners?.Invoke(arg1, arg2); // Broadcasts event for listeners to receive
    }

    private void OnDisable()
    {
        Listeners = null; // Safety net in case Unsubscribe() is not called
    }
}

/// <summary>
/// Generic Event Channel that takes in 3 arguments
/// </summary>
public class Event<T1, T2, T3> : ScriptableObject
{
    private Action<T1, T2, T3> Listeners;

    /// <inheritdoc cref="Event.Subscribe(Action)"/>
    public void Subscribe(Action<T1, T2, T3> function)
    {
        Listeners += function; // Literally adds a function that's triggered upon event being raised
    }

    /// <inheritdoc cref="Event.Unsubscribe(Action)"/>
    public void Unsubscribe(Action<T1, T2, T3> function)
    {
        Listeners -= function; // Removes function to be triggered upon event being raised
    }

    /// <inheritdoc cref="Event.Raise"/>
    public void Raise(T1 arg1, T2 arg2, T3 arg3)
    {
        Listeners?.Invoke(arg1, arg2, arg3); // Broadcasts event for listeners to receive
    }

    private void OnDisable()
    {
        Listeners = null; // Safety net in case Unsubscribe() is not called
    }
}