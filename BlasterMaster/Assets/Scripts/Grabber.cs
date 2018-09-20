using UnityEngine;

public class Grabber : MonoBehaviour
{
    // Declare variables.
    private OVRGrabbable _ovrGrabbableScript;
    [SerializeField] private Transform _leftHandAnchorTransform;
    [SerializeField] private Transform _rightHandAnchorTransform;

    private void Awake()
    {
        // Get the various components.
        _ovrGrabbableScript = GetComponent<OVRGrabbable>();
    }

    private void Start()
    {
        // Disable particle.
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
    // *****************************************************************************************
    private void FixedUpdate()
    {
        if (_ovrGrabbableScript.isGrabbed)
        {
            transform.GetChild(0).gameObject.SetActive(false);

            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0)
            {
                transform.position = _leftHandAnchorTransform.position;
            }
            else if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0)
            {
                transform.position = _rightHandAnchorTransform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            transform.GetChild(0).gameObject.SetActive(false);
    }
}