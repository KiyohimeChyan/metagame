using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckPoint : MonoBehaviour
{
    public GameObject checkPoint;
    public UIManager uIManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        uIManager.isPaused = true;
        checkPoint.SetActive(true);
    }
}
