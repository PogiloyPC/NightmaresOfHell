using UnityEngine;

public class FieldSpawnEnemy : MonoBehaviour
{
    [SerializeField] private Vector2Int _sizeField;

    public Vector2Int SizeField => _sizeField;

    public Vector3 PositionField() => transform.position;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3Int(_sizeField.x, 1, _sizeField.y));
    }
}

