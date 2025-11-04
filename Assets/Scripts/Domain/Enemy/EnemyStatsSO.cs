using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats")]
public class EnemyStatsSO : ScriptableObject
{
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
