using UnityEngine;

public class OlmecBoss : MonoBehaviour
{
    enum BOSSSTATES
    {
        REST,
        SPRAY,
        LAZER,
        POUND = 3
    }

    private BOSSSTATES currState = 0;
    private GameObject Eye, Mouth;
    private LazerEyeAttack LazerEye;
    private SprayAttack Spray;

    private void Awake()
    {
        LazerEye = GetComponentInChildren<LazerEyeAttack>();
        Eye = LazerEye.gameObject;
        Spray = GetComponentInChildren<SprayAttack>();
        Mouth = Spray.gameObject;
    }

    void Update()
    {
        switch (currState)
        {
            case BOSSSTATES.REST:

                break;

            case BOSSSTATES.SPRAY:

                break;

            case BOSSSTATES.LAZER:

                break;

            case BOSSSTATES.POUND:

                break;
        }
    }
}
