using UnityEngine;

public class FlickeringEmission : MonoBehaviour {

    public Material material;
    public bool on = false;

    private void Awake()
    {
        material.DisableKeyword("_EMISSION");
    }

    void Update () {
        if (on)
            material.EnableKeyword("_EMISSION");
        else
            material.DisableKeyword("_EMISSION");
    }
}
