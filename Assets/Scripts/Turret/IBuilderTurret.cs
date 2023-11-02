using UnityEngine;

public interface IBuilderTurret<T> : ICreaterBuild
{
    public bool BuildTurret(T turret);
}

public interface ICreaterExtension : ICreaterBuild
{
    public void BuildExtension();
}

public interface ICreaterBuild
{
    public Vector3 GetCurrentPositionBuild(); 
}