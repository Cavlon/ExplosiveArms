using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{

    [SerializeField] int sceneNo;

    public void LoadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(sceneNo);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exited Game");
    }
}
