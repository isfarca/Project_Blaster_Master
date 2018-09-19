using UnityEngine;

public class Endpoint : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private GameObject[] _lights;
    [SerializeField] private Animator[] _doors;
    [SerializeField] private Lever _leverScript;

    /// <summary>
    /// Load default states.
    /// </summary>
    private void Awake()
    {
        // Disable lights.
        if (_lights.Length > 0)
        {
            foreach (GameObject current in _lights)
                current.SetActive(false);
        }
    }

    /// <summary>
    /// Enable sirens and open the doors.
    /// </summary>
    /// <param name="other">Lever.</param>
    private void OnTriggerEnter(Collider other)
    {
        // If not a lever, than close the function.
        if (!other.gameObject.CompareTag("Lever"))
            return;

        // Enable lights.
        if (_lights.Length > 0)
        {
            foreach (GameObject current in _lights)
                current.SetActive(true);
        }

        // Open the doors.
        if (_doors.Length > 0)
        {
            foreach (Animator current in _doors)
            {
                current.enabled = true;
                current.Play(current.name);
            }
        }

        // Freeze the game object and disable the interactable.
        other.GetComponent<Rigidbody>().isKinematic = true;
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        other.GetComponent<BoxCollider>().enabled = false;
        other.GetComponent<OVRGrabbable>().enabled = false;

        // Delete the scripts.
        Destroy(_leverScript);
        Destroy(this);
    }
}