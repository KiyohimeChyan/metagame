using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockLight : MonoBehaviour
{
    public PlayerInputControlls inputControll;
    SpriteRenderer sprite;
    public Sprite idleImage;
    public Sprite glowImage;
    private void Awake()
    {
        inputControll = new PlayerInputControlls();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        inputControll.Enable();
    }

    private void OnDisable()
    {
        inputControll.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inputControll.Gameplay.Attack.started += LightSymbol;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inputControll.Gameplay.Attack.started -= LightSymbol;

    }

    private void LightSymbol(InputAction.CallbackContext obj)
    {
        sprite.sprite = glowImage;
    }
}
