using UnityEngine;

public class ZombieRagDoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform[] bones;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int ragDollLayer;
    [SerializeField] private bool usePhysics;
    private bool _isAlive = true;

    private void OnValidate()
    {
        MakeRagDoll(usePhysics);
    }
    private void Update()
    {
        if (!_isAlive)
            return;
        Vector3 targetPos = transform.position + Vector3.right * transform.localScale.x;
       transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            MakeRagDoll(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            transform.localScale = new Vector3(transform.localScale.x*-1, 1,1);
        }
    }
    public void MakeRagDoll(bool usePhysics)
    {
        _isAlive = !usePhysics;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.isKinematic = usePhysics;
        myCollider.enabled = !usePhysics;
        animator.enabled = !usePhysics;
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].gameObject.layer = ragDollLayer;
            bones[i].TryGetComponent(out Rigidbody2D _rigidBody);
            bones[i].TryGetComponent(out BoxCollider2D _collider);
            bones[i].TryGetComponent(out HingeJoint2D joint);
            if (_rigidBody)
            {
                _rigidBody.mass = 0.0001f;
                _rigidBody.simulated = usePhysics;
                _rigidBody.isKinematic = !usePhysics;
            }
            if (_collider)
                _collider.enabled = usePhysics;
            if (joint)
                joint.enabled = usePhysics;
        }
    }
}
