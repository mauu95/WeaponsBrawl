using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnimationController : NetworkBehaviour {

    public Animator PunchAnim;

    public Animator anim;
    public PlayerMovement mov;

    private void Start()
    {
        anim = GetComponent<Animator>();
        mov= GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!mov.enabled)
        {
            anim.SetFloat("Blend", 0f);
        }
    }

    public void PlayPunchAnimation()
    {
        StartCoroutine(PlayPunch());
    }

    IEnumerator PlayPunch()
    {
        CmdPlayPunch(true);
        yield return new WaitForSeconds(PunchAnim.GetCurrentAnimatorStateInfo(0).length);
        CmdPlayPunch(false);
    }

    [Command]
    void CmdPlayPunch(bool yesno)
    {
        RpcPlayPunch(yesno);
    }

    [ClientRpc]
    private void RpcPlayPunch(bool yesno)
    {
        PunchAnim.SetBool("isPunching", yesno);
    }
}
