using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BulletScript : ExplosiveBullet
{

    new void Start()
    {
        base.Start();
        rb.velocity = transform.right * speed;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        ExplodeCircle();
    }

}
