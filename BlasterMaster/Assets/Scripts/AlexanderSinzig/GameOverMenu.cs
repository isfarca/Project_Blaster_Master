using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    //[SerializeField] private BackToMainMenu _levelLoaderScript;

    public void GoToMainMenu()
    {
        //StartCoroutine(_levelLoaderScript.LoadingProgress(0));

        SceneManager.LoadScene(0);
    }
}
