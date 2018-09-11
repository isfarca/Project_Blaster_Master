using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Declare variables.
    private bool _vibrationActive, _rotationActive;
    [SerializeField] private GameObject _onOffCheckmark;
    [SerializeField] private LevelLoader _levelLoaderScript;

    private void Start()
    {
        _vibrationActive = _rotationActive = true;
    }

    public void GoToCampaign()
    {
        Debug.Log("Go to campaign");
        StartCoroutine(_levelLoaderScript.LoadingProgress(1));
    }

    public void GoToTutorial()
    {
        Debug.Log("Go to tutorial");
        StartCoroutine(_levelLoaderScript.LoadingProgress(1));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetVibration()
    {
        _vibrationActive = _vibrationActive ? false : true;
        _onOffCheckmark.gameObject.SetActive(_vibrationActive);
    }

    public void SetRotation()
    {
        _rotationActive = _rotationActive ? false : true;
        _onOffCheckmark.gameObject.SetActive(_rotationActive);
    }
}