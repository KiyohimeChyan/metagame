using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Event", menuName = "Character Stats/Void Event")]
public class VoidEventSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaisedEvent()
    {
        OnEventRaised?.Invoke();
    }
}
