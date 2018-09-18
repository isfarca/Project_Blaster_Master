using UnityEngine;

public class Signpost : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private ParticleSystem _signPost;
    [SerializeField] private GameObject[] _points;
    private GameObject _currentPoint;
    private int _pointsIndex;

    /// <summary>
    /// Set the start point from player.
    /// </summary>
    private void Start()
    {
        _pointsIndex = 0;

        _signPost.transform.position = _points[_pointsIndex].transform.position;
    }

    /// <summary>
    /// Particle go to goal and reset the particle position.
    /// </summary>
    private void FixedUpdate()
    {
        // Prevent the out of range from index the sign post array.
        if (_pointsIndex > _points.Length - 1)
            return;

        // Prevent null reference exception.
        if (_currentPoint == null || _points[_pointsIndex] == null)
            return;

        // Set the position and rotation of particle by depending on the direction.
        if (_signPost.transform.position.x < _points[_pointsIndex].transform.position.x)
        {
            _signPost.transform.position = new Vector3(_signPost.transform.position.x + 1, _signPost.transform.position.y, _signPost.transform.position.z);
            _signPost.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
        else if (_signPost.transform.position.x > _points[_pointsIndex].transform.position.x)
        {
            _signPost.transform.position = new Vector3(_signPost.transform.position.x - 1, _signPost.transform.position.y, _signPost.transform.position.z);
            _signPost.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
        else if (_signPost.transform.position.y < _points[_pointsIndex].transform.position.y)
        {
            _signPost.transform.position = new Vector3(_signPost.transform.position.x, _signPost.transform.position.y + 1, _signPost.transform.position.z);
            _signPost.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        }
        else if (_signPost.transform.position.y > _points[_pointsIndex].transform.position.y)
        {
            _signPost.transform.position = new Vector3(_signPost.transform.position.x, _signPost.transform.position.y - 1, _signPost.transform.position.z);
            _signPost.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        }
        else if (_signPost.transform.position.z < _points[_pointsIndex].transform.position.z)
        {
            _signPost.transform.position = new Vector3(_signPost.transform.position.x, _signPost.transform.position.y, _signPost.transform.position.z + 1);
            _signPost.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else if (_signPost.transform.position.z > _points[_pointsIndex].transform.position.z)
        {
            _signPost.transform.position = new Vector3(_signPost.transform.position.x, _signPost.transform.position.y, _signPost.transform.position.z - 1);
            _signPost.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else
        {
            _signPost.transform.position = _currentPoint.transform.position;
            _signPost.transform.rotation = Quaternion.identity;
        }
    }

    /// <summary>
    /// Set goal for the particle.
    /// </summary>
    public void SetGoal()
    {
        _points[_pointsIndex].GetComponent<BoxCollider>().enabled = false;
        _points[_pointsIndex + 1].GetComponent<BoxCollider>().enabled = true;

        _signPost.transform.position = _points[_pointsIndex].transform.position;
        _currentPoint = _points[_pointsIndex];
        _pointsIndex++;
    }
}