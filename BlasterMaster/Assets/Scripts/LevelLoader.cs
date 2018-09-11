using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private Text _progressText;

    private void Start()
    {
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

        _loadingSlider.gameObject.SetActive(true);

        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);

            _loadingSlider.value = progress;
            _progressText.text = progress * 100.0f + "%";

            yield return null;
        }

        StopAllCoroutines();
    }
}