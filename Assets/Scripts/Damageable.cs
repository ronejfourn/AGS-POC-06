using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
    }

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
                _health = 0;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool("IsAlive", value);
        }
    }

    [SerializeField]
    private bool _isHit = false;
    public bool IsHit
    {
        get { return _isHit; }
        set { animator.SetBool("IsHit", value); }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Hit(int dmg)
    {
        if (IsAlive)
        {
            IsHit = true;
            Health -= dmg;
        }
    }
}
