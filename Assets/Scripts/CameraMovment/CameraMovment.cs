using UnityEngine;

class CameraMovment : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _speedRotation;

    private void FixedUpdate()
    {
        float rotation = 0f;       

        if (Input.GetKey(KeyCode.Mouse2))
            rotation = Input.GetAxis("Mouse X") * _speedRotation;

        _rb.angularVelocity = new Vector3(0f, rotation, 0f);
    }
}

