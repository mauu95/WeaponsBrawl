using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletScript : MonoBehaviour {

    public float speed = 20f;
    public Rigidbody2D rb;
    [Range(2, 60)] [SerializeField] public int ExplosionRadius = 2;
    public Tilemap map;

    public GameObject explosionEffect;

    void Start () {
        rb.velocity = transform.right * speed;
        //rb.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        map = FindObjectOfType<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ExplodeCircle();
        Destroy(gameObject);
    }

    void ExplodeCircle()
    {
        foreach (var p in new BoundsInt(-ExplosionRadius, -ExplosionRadius, 0, 2 * ExplosionRadius + 1, 2 * ExplosionRadius + 1, 1).allPositionsWithin)
        {
            int x = p[0];
            int y = p[1];
            if (x * x + y * y - ExplosionRadius * ExplosionRadius < 0)
            {
                Vector3Int position = Vector3Int.FloorToInt(gameObject.transform.position);
                position.z = 0; // A volte diventa -1 a caso
                map.SetTile( position + p, null);
            }
        }
        explosionEffect.transform.localScale = new Vector3(ExplosionRadius, ExplosionRadius, 0);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }
}
