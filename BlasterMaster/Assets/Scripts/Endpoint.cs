using UnityEngine;

public class Endpoint : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private Light[] _lights;
    [SerializeField] private Animator[] _doors;
    [SerializeField] private Lever _leverScript;

    /// <summary>
    /// Enable sirens and open the doors.
    /// </summary>
    /// <param name="other">Lever.</param>
    private void OnTriggerEnter(Collider other)
    {
        // If not a lever, than close the function.
        if (!other.gameObject.CompareTag("Lever"))
            return;

        Debug.Log("Light and Door open!");

        // Enable lights.
        if (_lights.Length > 0)
        {
            foreach (Light current in _lights)
                current.enabled = true;
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