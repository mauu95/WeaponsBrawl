using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BulletScript : NetworkBehaviour
{

    [Range(2, 60)] [SerializeField] public int ExplosionRadius = 2;

    public int BulletPower = 20;
    public int FlingIntensity = 10;
    public float speed = 20f;

    public Rigidbody2D rb;
    public MapController map;
    public GameObject explosionEffect;


    void Start()
    {
        rb.velocity = transform.right * speed;
        map = FindObjectOfType<MapController>();
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        ExplodeCircle();
    }





    void ExplodeCircle()
    {
        DestroyMapCircle();
        CmdDamageWhoIsInsideTheExplosion();
        CmdFlingWhoIsInsideTheExplosion();
        CmdExplosionAnimation();
    }



    private void DestroyMapCircle()
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
    }





    void CmdFlingWhoIsInsideTheExplosion()
    {
        Collider2D[] hittedList = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach (Collider2D hitted in hittedList)
        {
            if (hitted.CompareTag("Player"))
            {
                Vector3 heading = hitted.transform.position - transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance;
                hitted.gameObject.GetComponent<Movement>().enabled = false;
                hitted.gameObject.GetComponent<Rigidbody2D>().velocity = direction * FlingIntensity;
                hitted.gameObject.GetComponent<PlayerManager>().ActivateMovementAfterSec();
            }
        }
    }




    [Command]
    void CmdExplosionAnimation()
    {
        ExplosionAnimation();
        RpcExplosionAnimation();
    }




    [ClientRpc]
    void RpcExplosionAnimation()
    {
        ExplosionAnimation();
    }




    private void ExplosionAnimation()
    {
        explosionEffect.transform.localScale = new Vector3(ExplosionRadius, ExplosionRadius, 0);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }




    [Command]
    void CmdDamageWhoIsInsideTheExplosion()
    {
        Collider2D[] hittedList = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach (Collider2D hitted in hittedList)
        {
            if (hitted.CompareTag("Player"))
            {
                hitted.gameObject.GetComponent<PlayerHealth>().CmdTakeDamage(BulletPower);
            }
        }
    }




    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
