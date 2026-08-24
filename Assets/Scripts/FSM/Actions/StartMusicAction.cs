using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StartMusic", menuName = "ScriptableObjects/FSM/Actions/StartMusic")]
public class StartMusicAction : StateAction
{
    [SerializeField] private EventAudioClip onBGMRequestEvent;
    [SerializeField] private AudioClip bgmClip;
    
    public override void Act(StateController controller)
    {
        onBGMRequestEvent.Raise(bgmClip);
    }
}
