using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private GameObject[] _fillOnGazeSlider;
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private Text _progressText;

    /// <summary>
    /// Deactivate the game objects, that you do not need for the beginning.
    /// </summary>
    private void Start()
    {
        // Disable the loading bar.
        _loadingSlider.gameObject.SetActive(false);
    }

    /// <summary>
    /// Level loading process.
    /// </summary>
    /// <param name="level">Scene build index.</param>
    /// <returns>NULL (irrelevant).</returns>
    public IEnumerator LoadingProgress(int level)
    {
        // Declare variables.
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        // Disable all buttons by level charging.
        foreach (GameObject currentSlider in _fillOnGazeSlider)
            currentSlider.gameObject.SetActive(false);

        // Loading slider it can be see.
        _loadingSlider.gameObject.SetActive(true);

        // Level charging.
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);

            _loadingSlider.value = progress;
            _progressText.text = progress * 100.0f + "%";

            yield return null;
        }

        // Stop level charging and go to level.
        StopAllCoroutines();
    }
}