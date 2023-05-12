using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    [Header("角色移动数据")]
    public PlayerStats_SO templateData;
    PlayerStats_SO playerMoveData;

    public float MoveSpeed
    {
        get
        {
            if (playerMoveData != null)
                return playerMoveData.moveSpeed;
            else return 0;
        }
        set
        {
            playerMoveData.moveSpeed = value;
        }
    }

    public float JumpForce
    {
        get
        {
            if (playerMoveData != null)
                return playerMoveData.jumpForce;
            else return 0;
        }
        set
        {
            playerMoveData.jumpForce = value;
        }
    }

    public float SlideDistance
    {
        get
        {
            if (playerMoveData != null)
                return playerMoveData.slideDistance;
            else return 0;
        }
        set
        {
            playerMoveData.slideDistance = value;
        }
    }

    private void Awake()
    {
        if (templateData != null)
        {
            playerMoveData = Instantiate(templateData);
        }

    }
}
