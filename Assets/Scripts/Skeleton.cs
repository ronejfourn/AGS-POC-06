using UnityEngine;

[RequireComponent(typeof(DetectWall), typeof(DetectGround), typeof(Respawn))]
public class Skeleton : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 3f;
    [SerializeField]
    private ColliderDetector playerDetector;
    [SerializeField]
    private ColliderDetector groundDetector;
    private Rigidbody2D rb;
    private Vector2 walkDirnVector = Vector2.right;
    private DetectGround dg;
    private DetectWall dw;
    private Animator animator;
    private Respawn respawn;
    private Damageable dmg;

    public enum Direction { right, left };

    [SerializeField]
    private Direction _walkDirection;
    public Direction WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
                gameObject.transform.localScale *= new Vector2(-1, 1);
            walkDirnVector = value == Direction.right ? Vector2.right : Vector2.left;
            _walkDirection = value;
        }
    }

    [SerializeField]
    private bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool("HasTarget", value);
        }
    }

    private bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool("CanMove"); }
    }

    public float AtkCooldown
    {
        get { return animator.GetFloat("AtkCooldown"); }
        set { animator.SetFloat("AtkCooldown", value < 0 ? 0 : value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dg = GetComponent<DetectGround>();
        dw = GetComponent<DetectWall>();
        animator = GetComponent<Animator>();
        dmg = GetComponent<Damageable>();
        respawn = GetComponent<Respawn>();
    }

    private void Update()
    {
        if (dmg.IsAlive)
        {
            HasTarget = playerDetector.colliders.Count > 0;
            if (AtkCooldown > 0)
                AtkCooldown -= Time.deltaTime;
        }
        respawn.respawnPoint = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        if (dg.IsOnGround && dw.IsOnWall || groundDetector.colliders.Count == 0)
            WalkDirection = (WalkDirection == Direction.right) ? Direction.left : Direction.right;
        if (CanMove)
            rb.velocity = new Vector2(walkDirnVector.x * walkSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
        IsMoving = rb.velocity.x != 0;
    }
}
