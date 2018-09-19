using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private GameObject _onOff;
    [SerializeField] private LevelLoader _levelLoaderScript;

    /// <summary>
    /// Start the main menu with default values.
    /// </summary>
    private void Start()
    {
        Setting.Vibration = Setting.Rotation = true;
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
    /// Toggle feedback.
    /// </summary>
    public void SetVibration()
    {
        Setting.Vibration = !Setting.Vibration;
        _onOff.gameObject.SetActive(Setting.Vibration);
    }

    /// <summary>
    /// Toggle rotation with stick setting.
    /// </summary>
    public void SetRotation()
    {
        Setting.Rotation = !Setting.Rotation;
        _onOff.gameObject.SetActive(Setting.Rotation);
    }
}