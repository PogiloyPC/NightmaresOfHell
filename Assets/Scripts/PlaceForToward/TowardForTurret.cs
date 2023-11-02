using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class TowardForTurret : PlaceForTurret
{
    [SerializeField] private Point _mainToward;

    [SerializeField] private float _positionYRay;

    [SerializeField] private Quaternion _rotationExtensionX;
    [SerializeField] private Quaternion _rotationExtensionZ;
    [SerializeField] private Quaternion _rotationExtension_X;
    [SerializeField] private Quaternion _rotationExtension_Z;

    private float _radius;

    private void Start()
    {        
        _radius = GetComponent<CapsuleCollider>().radius;
    }

    private void Update()
    {        
        Debug.DrawRay(new Vector3(transform.position.x + _radius + 0.15f, transform.position.y + _positionYRay, transform.position.z), transform.right);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z + _radius + 0.15f), transform.forward);
        Debug.DrawRay(new Vector3(transform.position.x - _radius + 0.15f, transform.position.y + _positionYRay, transform.position.z), -transform.right);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z - _radius + 0.15f), -transform.forward);
    }

    public bool CheckSpace(out Vector3 position, out Quaternion rotation)
    {
        RaycastHit hit;

        position = new Vector3();
        rotation = new Quaternion();

        Ray rayX = new Ray(new Vector3(transform.position.x + _radius / 2, transform.position.y + _positionYRay, transform.position.z), transform.right/2);
        Ray rayZ = new Ray(new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z + _radius / 2), transform.forward/2);
        Ray ray_X = new Ray(new Vector3(transform.position.x - _radius / 2, transform.position.y + _positionYRay, transform.position.z), -transform.right/2);
        Ray ray_Z = new Ray(new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z - _radius / 2), -transform.forward/2);       

        if (!Physics.Raycast(rayX, _radius + 0.1f))
        {            
            position = new Vector3(transform.position.x + _radius, transform.position.y + _positionYRay, transform.position.z);

            rotation = _rotationExtensionX;
            
            return true;
        }
        else if (!Physics.Raycast(rayZ, out hit, _radius + 0.1f))
        {
            position = new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z + _radius);

            rotation = _rotationExtensionZ;
            
            return true;
        }
        else if (!Physics.Raycast(ray_X, out hit, _radius + 0.1f))
        {
            position = new Vector3(transform.position.x - _radius, transform.position.y + _positionYRay, transform.position.z);

            rotation = _rotationExtension_X;
            
            return true;
        }
        else if (!Physics.Raycast(ray_Z, out hit, _radius + 0.1f))
        {
            position = new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z - _radius);

            rotation = _rotationExtension_Z;
            
            return true;
        }
        else
        {            
            return false;
        }
    }
}
