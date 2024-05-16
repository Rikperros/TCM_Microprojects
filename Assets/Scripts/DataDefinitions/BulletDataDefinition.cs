using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Definition", menuName = "Weapon/Add Bullet Definition")]

public class BulletDataDefinition : ScriptableObject
{
    [Tooltip("Bullet Speed in u/s")]
    public float speed = 25f;
    [Tooltip("Bullet damage. This won't be used in this example")]
    public float damage = 100f;
    [Tooltip("Bullet Color. This won't be used in this example")]
    public Color BulletColor = Color.white;
}
