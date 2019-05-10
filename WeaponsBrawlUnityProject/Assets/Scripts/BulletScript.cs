using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour {

    public float speed = 20f;
    public Rigidbody2D rb;
    [Range(2, 60)] [SerializeField] public int ExplosionRadius = 2;
    public MapController map;

    public GameObject explosionEffect;

    void Start () {
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
                position.z = 0; // A volte diventa -1 a caso
                Vector3Int destroyPos = position + p;
                map.CmdDestroyTile( destroyPos.x, destroyPos.y, destroyPos.z);
            }
        }
        explosionEffect.transform.localScale = new Vector3(ExplosionRadius, ExplosionRadius, 0);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }

    
}
