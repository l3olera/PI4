using System;
using UnityEngine;

[Serializable]
public enum IdEnemy
{
    Goblin,
    Cyclop,
    Rat,
    Ghost,
    Orc
}

[CreateAssetMenu(menuName = "Enemy/Stats")]
public class EnemyStatsSO : ScriptableObject
{
    public IdEnemy idEnemy;
    public int maxHealth;
    public float moveSpeed;
    public float acceleration;
    public int damage;
    public float attackInterval;
    public float attackRange;
    public float visionRange;
    public float visionAngle;
    public int soulReward;
    public bool canAlwaysSee;
}
