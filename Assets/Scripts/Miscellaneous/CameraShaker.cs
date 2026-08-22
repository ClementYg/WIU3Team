using UnityEngine;
using Unity.Cinemachine;

public class CameraShaker : MonoBehaviour
{
    [Header("Camera Shaker")]
    [SerializeField] CinemachineImpulseSource source;

    public void SetSustainTime(float sustainTime = 0.2f)
    {
        source.ImpulseDefinition.TimeEnvelope.SustainTime = sustainTime;
    }

    public void DoShake()
    {
        source.GenerateImpulse();
    }
}
