using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Killzone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        Health health = go.GetComponent<Health>();
        health.Damage(int.MaxValue);
    }
}
