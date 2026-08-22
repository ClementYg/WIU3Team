using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public abstract class CutsceneStep
{
    public bool blocking = true;
    public abstract IEnumerator Execute();
}
