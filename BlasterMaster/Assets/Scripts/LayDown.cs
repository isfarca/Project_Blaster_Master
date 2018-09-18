using UnityEngine;

public class LayDown : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private Transform _offset;
    private Feedback _feedbackScriptLeft, _feedbackScriptRight;

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
    /// Usable game object.
    /// </summary>
    /// <param name="other">Game object with usable tag.</param>
    private void OnTriggerStay(Collider other)
    {
        // The game object match, then continue usable logic.
        if (!other.gameObject.CompareTag("Usable"))
            return;

        // If no grab button pressed, than usable game object.
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) 
            return;
        
        other.GetComponent<Rigidbody>().isKinematic = true;
        other.transform.position = _offset.position;
        other.transform.rotation = Quaternion.identity;
        other.transform.parent = transform;
        other.GetComponent<SphereCollider>().enabled = false;
        other.GetComponent<OVRGrabbable>().enabled = false;
        other.GetComponent<Grabber>().enabled = false;
        other.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        _feedbackScriptLeft.Vibrate(VibrationForce.Hard);
        _feedbackScriptRight.Vibrate(VibrationForce.Hard);

        Destroy(this);
    }
}