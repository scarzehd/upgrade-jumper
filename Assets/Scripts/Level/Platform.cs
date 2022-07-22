using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D effector;

    [SerializeField] private PlayerController player;
    [SerializeField] private float waitTime;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (player.moveY < 0)
        {
            effector.surfaceArc = 0;
            waitTime = 0.2f;
        } else if (waitTime > 0f)
        {
            waitTime -= Time.deltaTime;
        } else if (effector.surfaceArc == 0)
        {
            effector.surfaceArc = 130;
        }
    }
}
