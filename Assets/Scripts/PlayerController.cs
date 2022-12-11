using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DetectGround), typeof(Damageable), typeof(Respawn))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float runSpeed = 5.0f;
    [SerializeField]
    private float jumpImpulse = 10.0f;

    private bool canJump = true;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    private DetectGround dg;
    private Damageable dmg;
    private Respawn respawn;

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

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
                transform.localScale *= new Vector2(-1, 1);
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool("CanMove"); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dg = GetComponent<DetectGround>();
        dmg = GetComponent<Damageable>();
        respawn = GetComponent<Respawn>();
        respawn.respawnPoint = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        float moveSpeed = 0;
        if (CanMove && !dmg.IsHit)
        {
            moveSpeed = moveInput.x * runSpeed;
            SetFacingDirection();
        }
        rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
        animator.SetFloat("YVel", rb.velocity.y);
    }

    private void SetFacingDirection()
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        IsMoving = moveInput.x != 0;
        canJump = moveInput.y >= 0;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started && dg.IsOnGround && CanMove && canJump)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            animator.SetTrigger("Attack");
        }
    }
}
