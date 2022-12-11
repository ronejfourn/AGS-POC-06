using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Respawn : MonoBehaviour
{
    [SerializeField]
    private float respawnCooldown = 5.0f;
    public Vector2 respawnPoint;
    private Damageable dmg;

    private void Awake()
    {
        dmg = GetComponent<Damageable>();
    }

    private void Update()
    {
        if (!dmg.IsAlive) StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(respawnCooldown);
        dmg.IsAlive = true;
        dmg.IsHit = false;
        dmg.Health = dmg.MaxHealth;
        gameObject.transform.position = respawnPoint;
    }
}