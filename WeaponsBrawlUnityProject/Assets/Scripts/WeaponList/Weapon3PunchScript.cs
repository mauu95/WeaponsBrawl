using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3PunchScript : AbstractWeaponGeneric
{
    public float attackRange;
    public int damagePower;
    public LayerMask PlayerLayer;

    public GameObject PunchGFX;
    public Animator anim;
    private bool GFXIsActive;
    private bool RenderIsActive = false;

    public PlayerAnimationController AnimationController;

    private void Update()
    {
        // Abilito l'oggetto PunchAnimation a seconda se lo sprite render di weapon3punch è abilitato o no
        RenderIsActive = GetComponent<SpriteRenderer>().enabled;
        if(GFXIsActive != RenderIsActive)
        {
            PunchGFX.SetActive(RenderIsActive);
            GFXIsActive = RenderIsActive;
        }
    }

    public override void Attack(int charge)
    {
        AnimationController.PlayPunchAnimation();

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
