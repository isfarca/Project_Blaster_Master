using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour {

    [SerializeField] private BackToMainMenu _levelLoaderScript;

    public void GoToMainMenu()
    {
        StartCoroutine(_levelLoaderScript.LoadingProgress(0));
    }
}
