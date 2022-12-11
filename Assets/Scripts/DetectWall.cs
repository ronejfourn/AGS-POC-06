using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class DetectWall : MonoBehaviour
{
    [SerializeField]
    private ContactFilter2D castFilter;
    [SerializeField]
    private float wallDistance = 0.1f;

    private CapsuleCollider2D capCollider;
    private RaycastHit2D[] wallHits = new RaycastHit2D[4];

    [SerializeField]
    private bool _isOnWall = true;
    public bool IsOnWall
    {
        get { return _isOnWall; }
        set { _isOnWall = value; }
    }

    private void Awake()
    {
        capCollider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        Vector2 wallCheckDirn = gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        IsOnWall = capCollider.Cast(wallCheckDirn, castFilter, wallHits, wallDistance) > 0;
    }
}
