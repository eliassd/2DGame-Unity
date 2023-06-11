using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    //Lista de observadores
    private List<IObserver> _observers = new List<IObserver>();

    public void addObserver(IObserver observer)
    {
        _observers.Add(observer);
    }
    public void removeObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void notifyObserver(PlayerActions action)
    {
        _observers.ForEach((_observers) => { _observers.OnNotify(action); });
    }
}
