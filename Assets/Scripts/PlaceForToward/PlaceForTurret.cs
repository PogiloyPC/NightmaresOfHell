using UnityEngine;

public abstract class PlaceForTurret : MonoBehaviour
{
    [SerializeField] private Point _positionForTurret;

    public Vector3 GetPositionForTurret() => _positionForTurret.GetPosition();
}