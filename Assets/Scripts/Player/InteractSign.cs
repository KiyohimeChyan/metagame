using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class InteractSign : MonoBehaviour
{
    public PlayerInputControlls inputs;
    public Transform playerTrans;
    private Animator anim;
    public GameObject signSprite;
    bool canPress;
    private IInteractable interactTarget;
    private void Awake()
    {
        anim = signSprite.GetComponent<Animator>();
        inputs = new PlayerInputControlls();
        inputs.Enable();
    }
    private void OnEnable()
    {
        //InputSystem.onActionChange += OnActionChange;
        inputs.Gameplay.Confirm.started += OnConfirm;
    }


    private void Update()
    {
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        signSprite.transform.localScale = playerTrans.localScale;
    }

    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        if(actionChange == InputActionChange.ActionStarted)
        {
            var currentDevice = ((InputAction)obj).activeControl.device;
            switch (currentDevice.device)
            {
                case Keyboard:
                    anim.Play("Keyboard");
                    break;
                case XInputControllerWindows:
                    anim.Play("Gamepad");
                    break;
            }
        }
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if (canPress)
        {
            interactTarget.TriggerAction();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canPress = true;
            interactTarget = collision.GetComponent<IInteractable>();
        }
        else if(collision.CompareTag("Untagged"))
        {
            canPress = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }

}
