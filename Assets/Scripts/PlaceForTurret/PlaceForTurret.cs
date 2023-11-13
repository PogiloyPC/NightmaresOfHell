using UnityEngine;

public abstract class PlaceForTurret : MonoBehaviour, IGameContent
{
    [SerializeField] private Point _positionForTurret;

    [SerializeField] private TypeGameContent _typeGameContent = TypeGameContent.placeForTurret;

    private bool _haveTurret;
    public bool HaveTurret => _haveTurret;

    public TypeGameContent GetTypeGameContent() => _typeGameContent;

    public Vector3 GetPositionForTurret() => _positionForTurret.GetPosition();

    public void SetTurret() => _haveTurret = true;
}