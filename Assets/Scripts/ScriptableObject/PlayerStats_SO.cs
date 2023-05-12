using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Player Stats")]
public class PlayerStats_SO : ScriptableObject
{
    public float moveSpeed;
    public float jumpForce;
    public float slideDistance;
}
