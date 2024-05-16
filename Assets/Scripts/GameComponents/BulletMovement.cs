using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    BulletDataDefinition BulletDataDefinition;

    public Vector3 movementDirection = Vector3.zero;

    private void Awake()
    {
        Debug.Assert(BulletDataDefinition != null, "Bullet lacks its data! Destroying the object...", this);

        if(BulletDataDefinition == null)
        {
            Destroy(this);
            return;
        }
    }
    void Update()
    {
        transform.position += movementDirection.normalized * BulletDataDefinition.speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
