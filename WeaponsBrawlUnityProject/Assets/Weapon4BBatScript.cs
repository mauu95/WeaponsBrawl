using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon4BBatScript : AbstractWeaponGeneric
{
    public PlayerAnimationController AnimationController;

    public override void Attack(int charge)
    {
        
        AnimationController.PlayBBatAnimation();
        
    }
}
