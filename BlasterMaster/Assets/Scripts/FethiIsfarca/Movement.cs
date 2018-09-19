using UnityEngine;

public class Movement : MonoBehaviour
{
    // Declare variables.
    private CharacterController _controller;
    private float _rotationSmooth, _tiltAngle;
    private Vector3 _moveForceLeft, _moveForceRight, _moveForce;
    [SerializeField] private Transform _comparisonTransform;
    [SerializeField] private Transform _leftHandAnchorTransform;
    [SerializeField] private Transform _rightHandAnchorTransform;

	/// <summary>
    /// Get the character controller script.
    /// </summary>
    private void Awake()
    {
		// Get the scripts and components.
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _rotationSmooth = 2.0f;
        _tiltAngle = 30.0f;
    }

    /// <summary>
    /// Movement with jet logic.
    /// </summary>
    private void Update()
    {
        // Movement in space.
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) // Left.
            _moveForceLeft += (_comparisonTransform.position - _leftHandAnchorTransform.position) / 160;
        else if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)) // Right.
            _moveForceRight += (_comparisonTransform.position - _rightHandAnchorTransform.position) / 160;

        // Set move force.
        _moveForce = _moveForceLeft + _moveForceRight;

        // Move the player with move force values.
        _controller.Move(_moveForce);

        // 0.5 per cent lost movement speed.
        _moveForceLeft *= 0.995f;
        _moveForceRight *= 0.995f;

        // Rotation in space.
        if (!Setting.Rotation)
            return;
        
        // Get the stick float values.
        Vector2 leftStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 rightStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        // Rotate the player, when his interact with stick.
        if (leftStick.x > 0 || leftStick.x < 0 || leftStick.y > 0 || leftStick.y < 0)
            RotatePlayer(leftStick.x, leftStick.y);
        else if (rightStick.x > 0 || rightStick.x < 0 || rightStick.y > 0 || rightStick.y < 0)
            RotatePlayer(rightStick.x, rightStick.y);
    }

    /// <summary>
    /// Rotate the player with linear interpolation.
    /// </summary>
    /// <param name="horizontal">Rotation z-axis.</param>
    /// <param name="vertical">Rotation x-axis.</param>
    private void RotatePlayer(float horizontal, float vertical)
    {
        // Declare variables.
        float tiltAroundZ = horizontal * _tiltAngle,
            tiltAroundX = vertical * _tiltAngle;
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        
        // Dampen towards the target rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _rotationSmooth);
    }
}