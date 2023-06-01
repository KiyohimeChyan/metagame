using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProcess : MonoBehaviour
{
    public Transform playerTrans;
    public Slider processSlider;
    float currentX;

    // Update is called once per frame
    void Update()
    {
        currentX = playerTrans.position.x;
        processSlider.value = currentX / 393.0f;
    }
}
