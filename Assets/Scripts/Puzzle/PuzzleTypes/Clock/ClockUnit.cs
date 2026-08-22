using UnityEngine;

public class ClockUnit : MonoBehaviour
{
    [SerializeField] int correctHour;
    [SerializeField] int correctMinute;
    [SerializeField] ClockHandInput hourHand;
    [SerializeField] ClockHandInput minuteHand;

    [Header("Listen to hand changes")]
    [SerializeField] EventFloat hourHandChangedEvent;   
    [SerializeField] EventFloat minuteHandChangedEvent; 

    public bool IsSolved { get; private set; }
    [SerializeField] public EventClockUnit OnSolvedEvent;
    [SerializeField] public EventClockUnit OnUnsolvedEvent;

    void OnEnable()
    {
        hourHandChangedEvent?.Subscribe(HandleHandChanged);
        minuteHandChangedEvent?.Subscribe(HandleHandChanged);
    }

    void OnDisable()
    {
        hourHandChangedEvent?.Unsubscribe(HandleHandChanged);
        minuteHandChangedEvent?.Unsubscribe(HandleHandChanged);
    }

    void HandleHandChanged(float _) => Evaluate();

    void Evaluate()
    {
        bool nowSolved = hourHand.GetCurrentValue() == correctHour
                       && minuteHand.GetCurrentValue() == correctMinute;
        if (nowSolved == IsSolved) return;

        IsSolved = nowSolved;
        if (IsSolved) OnSolvedEvent?.Raise(this);
        else OnUnsolvedEvent?.Raise(this);
    }

    public void SetTarget(int hour, int minute)
    {
        correctHour = hour;
        correctMinute = minute;
        Evaluate();
    }
}