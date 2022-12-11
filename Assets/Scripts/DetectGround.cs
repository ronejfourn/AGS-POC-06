using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Animator), typeof(Rigidbody2D))]
public class DetectGround : MonoBehaviour
{
    public ContactFilter2D castFilter;
    [SerializeField]
    private ContactFilter2D platformCastFilter;
    [SerializeField]
    private float groundDistance = 0.05f;
    [SerializeField]
    private float platformDistance = 0.2f;

    private CapsuleCollider2D capCollider;
    private Animator animator;
    private RaycastHit2D[] groundHits = new RaycastHit2D[4];
    private RaycastHit2D[] platformHits = new RaycastHit2D[4];
    [SerializeField]
    bool inPlatform = false;

    [SerializeField]
    private bool _isOnGround = true;
    public bool IsOnGround
    {
        get
        {
            return _isOnGround;
        }
        set
        {
            _isOnGround = value;
            animator.SetBool("IsOnGround", value);
        }
    }

    private void Awake()
    {
        capCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 wallCheckDirn = gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        if (!IsOnGround && !inPlatform)
        {
            inPlatform = capCollider.Cast(wallCheckDirn, platformCastFilter, platformHits, platformDistance) > 0;
            if (inPlatform)
            {
                castFilter.useLayerMask = true;
                StartCoroutine(LayerMaskOff());
            }
        }

        IsOnGround = capCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }

    private IEnumerator LayerMaskOff()
    {
        yield return new WaitForSeconds(0.1f);
        castFilter.useLayerMask = false;
        inPlatform = false;
    }
}
