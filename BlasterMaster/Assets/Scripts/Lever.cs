using UnityEngine;

public class Lever : MonoBehaviour
{
    // Declare variables.
    private Feedback _feedbackScriptLeft, _feedbackScriptRight;
    private OVRGrabbable _pluginGrabScript;

    /// <summary>
    /// Get the references.
    /// </summary>
    private void Awake()
    {
        // Get the scripts by name in hierarchy.
        _feedbackScriptLeft = GameObject.Find("FeedbackLeft").GetComponent<Feedback>();
        _feedbackScriptRight = GameObject.Find("FeedbackRight").GetComponent<Feedback>();
    }

    /// <summary>
    /// Vibration logic.
    /// </summary>
    /// <param name="other">Player.</param>
    private void OnTriggerStay(Collider other)
    {
        // If not a player, than close the function.
        if (!other.gameObject.CompareTag("Player"))
            return;

        // Press left hand trigger, than left controller vibrate, otherwise right controller vibrate.
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            _feedbackScriptLeft.Vibrate(VibrationForce.Hard);
        else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            _feedbackScriptRight.Vibrate(VibrationForce.Hard);
    }
}