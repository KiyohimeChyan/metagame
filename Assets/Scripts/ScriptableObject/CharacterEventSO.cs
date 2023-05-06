using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Event", menuName = "Character Stats/Event Data")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<CharacterStats> OnEventRaised;

    public void RaiseEvent(CharacterStats cs)
    {
        OnEventRaised?.Invoke(cs);
    }

}
