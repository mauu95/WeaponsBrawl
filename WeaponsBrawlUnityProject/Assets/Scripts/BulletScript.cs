﻿using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{

    [Range(2, 60)] [SerializeField] public int ExplosionRadius = 2;

    public float speed = 20f;
    public Rigidbody2D rb;
    public MapController map;
    public GameObject explosionEffect;
    public int BulletPower = 20;

    void Start()
    {
        rb.velocity = transform.right * speed;
        map = FindObjectOfType<MapController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CmdExplodeCircle();

        Destroy(gameObject);
    }

    [Command]
    void CmdExplodeCircle()
    {
        foreach (var p in new BoundsInt(-ExplosionRadius, -ExplosionRadius, 0, 2 * ExplosionRadius + 1, 2 * ExplosionRadius + 1, 1).allPositionsWithin)
        {
            int x = p[0];
            int y = p[1];
            if (x * x + y * y - ExplosionRadius * ExplosionRadius < 0)
            {
                Vector3Int position = Vector3Int.FloorToInt(gameObject.transform.position);
                position.z = 0; // A volte diventa -1 a caso quindi lo forzo a 0 io
                Vector3Int destroyPos = position + p;
                map.CmdDestroyTile(destroyPos.x, destroyPos.y, destroyPos.z);
            }
        }
        explosionEffect.transform.localScale = new Vector3(ExplosionRadius, ExplosionRadius, 0);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        DamageWhoIsInsideTheExplosion();
    }

    void DamageWhoIsInsideTheExplosion()
    {
        Collider2D[] hittedList = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach (Collider2D hitted in hittedList)
        {
            if (hitted.CompareTag("Player"))
            {
                hitted.gameObject.GetComponent<PlayerHealth>().TakeDamage(Mathf.FloorToInt(BulletPower / 2)); // divido per 2 perchè il player ha 2 collider e TakeDamage viene chiamato per ogni collider
            }
        }
    }

    //Gizmo explosion radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
