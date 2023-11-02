using UnityEngine;
using System.Collections.Generic;

public interface IFactory<T> : IFactorySpawn<T>, IFactoryReclaim<T> where T : GameMono
{

}

public interface IFactorySpawn<T> where T : GameMono
{
    public T GetInstance(T t);
}

public interface IFactoryReclaim<T> where T : GameMono
{
    public void Reclaim(T t);
}

public class Factory<T> : IFactory<T> where T : GameMono
{
    protected List<T> _objectsFactory = new List<T>();

    public T GetInstance(T objec)
    {
        T instance = GameObject.Instantiate(objec);

        _objectsFactory.Add(instance);

        return instance;
    }

    public void GameUpdate()
    {
        if (_objectsFactory.Count <= 0)
            return;

        foreach (GameMono objectFactory in _objectsFactory)
            objectFactory.GameUpdate();
    }

    public void Reclaim(T objectFactory)
    {
        DoingAction(objectFactory);

        _objectsFactory.Remove(objectFactory);

        GameObject.Destroy(objectFactory.gameObject);
    }

    protected virtual void DoingAction(T objectFactory)
    {

    }
}