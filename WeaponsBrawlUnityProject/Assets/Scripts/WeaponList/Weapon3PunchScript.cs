using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3PunchScript : AbstractWeaponGeneric
{
    public float attackRange;
    public int damagePower;
    public LayerMask PlayerLayer;

    public override void Attack(int charge)
    {
        RaycastHit2D hitted = Physics2D.Raycast(firePoint.position, firePoint.right, attackRange, PlayerLayer);

        Debug.DrawRay(new Vector3(firePoint.position.x, firePoint.position.y, 0), firePoint.right * attackRange, Color.yellow, 5f, true);

        if (hitted)
        {
            print(hitted.transform.name);

            PlayerHealth enemy = hitted.transform.GetComponent<PlayerHealth>();

            if (enemy)
                enemy.CmdTakeDamage(damagePower, Player.gameObject);
        }
    }
}
