/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    public PlayerController player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (player.playerMovement.move != 0)
        {
            anim.SetBool("moving", true);
        } else
        {
            anim.SetBool("moving", false);
        }

        if (player.playerMovement.isGrounded == false)
        {
            anim.SetBool("jumping", true);
        } else
        {
            anim.SetBool("jumping", false);
        }

        if (player.playerMovement.wallSliding == true)
        {
            anim.SetBool("sliding", true);
        } else
        {
            anim.SetBool("sliding", false);
        }

        if (Input.GetMouseButtonDown(0) && player.sword.damage > 0)
        {
            player.sword.gameObject.SetActive(true);
            player.playerMovement.canTurn = false;
        }
    }
}
*/