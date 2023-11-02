using UnityEngine;

[CreateAssetMenu(fileName = "ContainerTurret")]
public class ContainerTurret : ScriptableObject
{
    [SerializeField] private Turret _turretPrefab;

    public Turret Turret => _turretPrefab;
}
