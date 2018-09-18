using UnityEngine;

public class Point : MonoBehaviour
{
    // Declare variables.
    private Signpost _signPostScript;

    /// <summary>
    /// Get the various components.
    /// </summary>
    private void Awake()
    {
        // Get the reference of the sign post script.
        _signPostScript = GameObject.FindGameObjectWithTag("Signpost").GetComponent<Signpost>();
    }

    /// <summary>
    /// Call the goal function in sign post script.
    /// </summary>
    /// <param name="other">Player.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _signPostScript.SetGoal();
    }
}