using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckPoint : MonoBehaviour
{
    public GameObject checkPoint;
    public GameObject timeCount;
    public UIManager uIManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        timeCount.SetActive(true);
        uIManager.isPaused = true;
        checkPoint.SetActive(true);
    }
}
