using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [Header("Camera Shaker")]
    [SerializeField] CameraShake cmrShake;

    public void SetSustainTime(float sustainTime = 0.2f)
    {
        cmrShake.source.ImpulseDefinition.TimeEnvelope.SustainTime = sustainTime;
    }

    public void DoShake()
    {
        cmrShake.source.GenerateImpulse();
    }
}
