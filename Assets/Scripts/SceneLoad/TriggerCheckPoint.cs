using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckPoint : MonoBehaviour
{
    public GameObject checkPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        checkPoint.SetActive(true);
    }
}
