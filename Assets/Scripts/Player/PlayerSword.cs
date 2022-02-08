using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public Animator animator;
    public PlayerController player;

    public int damage = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void EndAttack()
    {
        gameObject.SetActive(false);
/*        player.playerMovement.canTurn = true;*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().Damage(damage);
        }
    }
}
