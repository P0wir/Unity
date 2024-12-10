using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    public float moveSpeed;
    public float maxHp;
    public float damage;
    public float exp;
    public float expToNextLevel;
    public float levelUpExpMultiplier;
}
