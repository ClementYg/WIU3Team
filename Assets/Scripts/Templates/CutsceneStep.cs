using UnityEngine;

public abstract class CutsceneStep
{
    public bool blocking = true;
    public abstract System.Collections.IEnumerator Execute(CutsceneManager ctx);
}
