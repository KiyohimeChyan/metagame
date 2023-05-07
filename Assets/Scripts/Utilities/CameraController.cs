using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineConfiner2D cinemachineConfiner;

    public CinemachineImpulseSource cIS;

    public VoidEventSO voidEvent;

    private void Awake()
    {
        cinemachineConfiner = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        voidEvent.OnEventRaised += OnCameraShakeEvent;
    }

    private void OnDisable()
    {
        voidEvent.OnEventRaised -= OnCameraShakeEvent;
    }

    private void Start()
    {
        GetNewConfiner();
    }

    private void OnCameraShakeEvent()
    {
        cIS.GenerateImpulse();
    }

    private void GetNewConfiner()
    {
        var obj = GameObject.FindGameObjectWithTag("Boundary");
        if (obj == null)
            return;
        cinemachineConfiner.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        cinemachineConfiner.InvalidateCache();
    }


}
