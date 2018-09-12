using UnityEngine;
using UnityEngine.XR;

public class Grabber : MonoBehaviour
{
    // Declare variables.
    private OVRGrabbable _ovrGrabbableScript;
    [SerializeField] private Transform _leftHandAnchorTransform;
    [SerializeField] private Transform _rightHandAnchorTransform;
    [SerializeField] private Transform _level;
    private Rigidbody _rigidbody;
    private int _rotationCount;
    private bool _isRotate;

    /// <summary>
    /// Get the references.
    /// </summary>
    private void Awake()
    {
        // Get the various components.
        _ovrGrabbableScript = GetComponent<OVRGrabbable>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Handle various objects.
    /// </summary>
    private void Start()
    {
        // Disable particle.
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        _rotationCount = 0;
        _isRotate = false;
    }

    /// <summary>
    /// Enable particle.
    /// </summary>
    /// <param name="other">Player.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    /// <summary>
    /// Grab logic for player.
    /// </summary>
    /// <param name="other">Player.</param>
    private void OnTriggerStay(Collider other)
    {
        // When in the trigger not player stay, than exit the function.
        if (!other.gameObject.CompareTag("Player"))
            return;

        // When a player grabbed, than set the position of this object equal hand.
        if (_ovrGrabbableScript.isGrabbed)
        {
            // Disable particle.
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

            // Disable physics to counteract the trample effect.
            _rigidbody.isKinematic = true;

            // When a player triggered a hand trigger button, than set the position of this object equal current hand and parent this object to the child object of the hand.
            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            {
                transform.position = _leftHandAnchorTransform.position;
                transform.parent = _leftHandAnchorTransform;

                _isRotate = _rotationCount > 0 ? false : true;
                _rotationCount = _rotationCount == 0 ? 1 : 1;
            }
            else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                transform.position = _rightHandAnchorTransform.position;
                transform.parent = _rightHandAnchorTransform;

                _isRotate = _rotationCount > 0 ? false : true;
                _rotationCount = _rotationCount == 0 ? 1 : 1;
            }
        }
        else // When a player not grabbed, than enable physics, parent this object to the child object of the level and enable particle.
        {
            _rigidbody.isKinematic = false;

            transform.parent = _level;

            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

            _isRotate = false;
            _rotationCount = 0;
        }

        // Set zero rotation.
        if (_isRotate)
            transform.rotation = Quaternion.identity;

        // Rotate the particle equal headset.
        Quaternion angles = InputTracking.GetLocalRotation(XRNode.CenterEye);
        transform.GetChild(0).GetChild(0).rotation = Quaternion.Euler(angles.eulerAngles.x, angles.eulerAngles.y, angles.eulerAngles.z);
    }

    /// <summary>
    /// Disable particle.
    /// </summary>
    /// <param name="other">Player.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
}