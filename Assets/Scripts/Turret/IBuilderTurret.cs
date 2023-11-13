using UnityEngine;

public interface IBuilderTurret<T> : ILounchPosition
{
    public bool BuildTurret(T turret);
}

public interface ICreaterExtension : ILounchPosition
{
    public void BuildExtension();
}

public interface ILounchPosition
{
    public Vector3 GetLounchPosition(); 
}