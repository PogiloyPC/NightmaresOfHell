using UnityEngine;

public class CircleWallToward : MonoBehaviour
{
    [SerializeField] private int _idWallCircle;

    public void InitWall(IToward toward)
    {
        if (_idWallCircle != toward.GetLevel())
            gameObject.SetActive(false);
    }

    public void SetEnabled(IToward toward)
    {
        if (gameObject.activeSelf)
            return;

        if (_idWallCircle == toward.GetLevel())
            gameObject.SetActive(true);
    }
}
