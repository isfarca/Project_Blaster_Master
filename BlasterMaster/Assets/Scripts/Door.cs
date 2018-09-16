using UnityEngine;

public class Door : MonoBehaviour 
{
    // Declare variables.
    private Transform _key1, _key2;
    private Animator _animator;

    /// <summary>
    /// Get the references.
    /// </summary>
    private void Awake()
    {
        _key1 = GameObject.FindGameObjectWithTag("Key1").GetComponent<Transform>();
        _key2 = GameObject.FindGameObjectWithTag("Key2").GetComponent<Transform>();

        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Open the door and destroy this script.
    /// </summary>
    private void Update()
    {
        if (!(_key1.eulerAngles.x >= 180.0f) || !(_key2.eulerAngles.x <= 0.0f)) 
            return;

        _animator.enabled = true;
        _animator.Play(gameObject.name);

        Destroy(this);
    }
}