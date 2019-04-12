using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickScript : MonoBehaviour
{
    public void SwitchScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit() {
        Application.Quit();
    }
}
