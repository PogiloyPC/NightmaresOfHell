using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class TowardForTurret : PlaceForTurret
{
    [SerializeField] private Quaternion _rotationExtensionX;
    [SerializeField] private Quaternion _rotationExtensionZ;
    [SerializeField] private Quaternion _rotationExtension_X;
    [SerializeField] private Quaternion _rotationExtension_Z;

    [SerializeField] private float _distanceRay;
    [SerializeField] private float _positionYRay;
    private float _radius;

    public bool HaveSpaceForExtension { get; private set; } = true;
    
    private void Start()
    {        
        _radius = GetComponent<CapsuleCollider>().radius;
    }   

    public bool CheckSpace(out Vector3 position, out Quaternion rotation)
    {        
        position = new Vector3();
        rotation = new Quaternion();       

        Ray rayX = new Ray(new Vector3(transform.position.x + _radius / 2, transform.position.y + _positionYRay, transform.position.z), transform.right/2);
        Ray rayZ = new Ray(new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z + _radius / 2), transform.forward/2);
        Ray ray_X = new Ray(new Vector3(transform.position.x - _radius / 2, transform.position.y + _positionYRay, transform.position.z), -transform.right/2);
        Ray ray_Z = new Ray(new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z - _radius / 2), -transform.forward/2);       

        if (!Physics.Raycast(rayX, _radius + _distanceRay))
        {            
            position = new Vector3(transform.position.x + _radius, transform.position.y + _positionYRay, transform.position.z);

            rotation = _rotationExtensionX;
            
            HaveSpaceForExtension = true;

            return HaveSpaceForExtension;
        }
        else if (!Physics.Raycast(rayZ, _radius + _distanceRay))
        {
            position = new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z + _radius);

            rotation = _rotationExtensionZ;
            
            HaveSpaceForExtension = true;

            return HaveSpaceForExtension;
        }
        else if (!Physics.Raycast(ray_X, _radius + _distanceRay))
        {
            position = new Vector3(transform.position.x - _radius, transform.position.y + _positionYRay, transform.position.z);

            rotation = _rotationExtension_X;
            
            HaveSpaceForExtension = true;

            return HaveSpaceForExtension;
        }
        else if (!Physics.Raycast(ray_Z, _radius + _distanceRay))
        {
            position = new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z - _radius);

            rotation = _rotationExtension_Z;

            HaveSpaceForExtension = true;

            return HaveSpaceForExtension;
        }
        else
        {
            HaveSpaceForExtension = false;

            return HaveSpaceForExtension;
        }
    }   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(transform.position.x + _radius + 0.15f, transform.position.y + _positionYRay, transform.position.z), transform.right);
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z + _radius + 0.15f), transform.forward);
        Gizmos.DrawRay(new Vector3(transform.position.x - _radius + 0.15f, transform.position.y + _positionYRay, transform.position.z), -transform.right);
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + _positionYRay, transform.position.z - _radius + 0.15f), -transform.forward);
    }
}
