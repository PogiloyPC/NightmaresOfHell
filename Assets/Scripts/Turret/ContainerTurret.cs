using UnityEngine;

[CreateAssetMenu(fileName = "ContainerTurret")]
public class ContainerTurret : ScriptableObject, ISeterPrice, IPurchasable
{
    [SerializeField] private Turret _turretPrefab;
    public Turret Turret => _turretPrefab;

    [SerializeField] private int _priceTurret;
    public int GetPrice() => _priceTurret;

    [SerializeField] private Sprite _imageTurret;
    public Sprite ImageTurret => _imageTurret;
}
