using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Declare variables.
    private bool _vibrationActive, _rotationActive;
    [SerializeField] private GameObject _onOffCheckmark;
    [SerializeField] private LevelLoader _levelLoaderScript;

    /// <summary>
    /// Start the main menu with default values.
    /// </summary>
    private void Start()
    {
        _vibrationActive = _rotationActive = true;
    }

    /// <summary>
    /// Spring to level.
    /// </summary>
    public void GoToCampaign()
    {
        StartCoroutine(_levelLoaderScript.LoadingProgress(1));
    }

    /// <summary>
    /// Spring to level.
    /// </summary>
    public void GoToTutorial()
    {
        StartCoroutine(_levelLoaderScript.LoadingProgress(1));
    }

    /// <summary>
    /// Close the game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Toggle haptic feedback.
    /// </summary>
    public void SetVibration()
    {
        _vibrationActive = _vibrationActive ? false : true;
        _onOffCheckmark.gameObject.SetActive(_vibrationActive);
    }

    /// <summary>
    /// Toggle rotation with stick setting.
    /// </summary>
    public void SetRotation()
    {
        _rotationActive = _rotationActive ? false : true;
        _onOffCheckmark.gameObject.SetActive(_rotationActive);
    }
}