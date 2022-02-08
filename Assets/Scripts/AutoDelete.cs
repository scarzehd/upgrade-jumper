using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    public float deathTime = 0;

    private void Awake()
    {
        StartCoroutine(DeathTimer());
    }

    public IEnumerator DeathTimer()
    {
        while (deathTime == 0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
