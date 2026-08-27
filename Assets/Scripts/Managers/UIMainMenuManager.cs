using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuManager : Singleton<UIMainMenuManager>
{
    public GameObject fadeBlack;
    public GameObject mainMenuPanel;

    public void OnPlay()
    {
        mainMenuPanel.GetComponent<UIFader>().FadeOut();
        fadeBlack.GetComponent<UIFader>().FadeIn(() => SceneManager.LoadScene("CentralHub"));
    }
}
