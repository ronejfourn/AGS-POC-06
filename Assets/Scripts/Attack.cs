using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int atkDmg = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable dmg = collision.GetComponent<Damageable>();
        if (dmg != null) dmg.Hit(atkDmg);
    }
}
