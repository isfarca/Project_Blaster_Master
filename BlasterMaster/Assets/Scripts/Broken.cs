using UnityEngine;

public class Broken : MonoBehaviour
{
    // Declare variables.
    private OculusHaptics _oculusHapticsScriptLeft, _oculusHapticsScriptRight;

    private void Awake()
    {
        _oculusHapticsScriptLeft = GameObject.Find("OculusHapticsLeft").GetComponent<OculusHaptics>();
        _oculusHapticsScriptRight = GameObject.Find("OculusHapticsRight").GetComponent<OculusHaptics>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.CompareTag("Broke") != other.gameObject.CompareTag("Broke"))
            return;

        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            _oculusHapticsScriptLeft.Vibrate(VibrationForce.Medium);
        else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            _oculusHapticsScriptRight.Vibrate(VibrationForce.Medium);

        Destroy(other.gameObject);
    }
}