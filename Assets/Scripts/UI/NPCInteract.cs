using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour,IInteractable
{
    //Ҫ��Ҫ�ٴν���
    public bool isDone;
    public GameObject NPCDialog;
    public PlayerController playerController;

    public void TriggerAction()
    {
        if (!isDone)
        {
            ShowNPCDialog();
        }
    }

    private void ShowNPCDialog()
    {
        NPCDialog.SetActive(true);
        //playerController.isReading = true;
        isDone = true;
        this.gameObject.tag = "Untagged";
    }
}
