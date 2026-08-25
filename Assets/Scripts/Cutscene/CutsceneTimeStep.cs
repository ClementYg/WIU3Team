using UnityEngine;
using System.Collections;

[System.Serializable]
public class CutsceneTimeStep : CutsceneStep
{
    public float secondsToWait;

    public override IEnumerator Execute()
    {
        yield return new WaitForSeconds(secondsToWait);
    }
}
