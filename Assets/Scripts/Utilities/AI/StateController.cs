using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainInState;

    public Pathfinding pathfinding;

    public List<Node> poi;
    public List<Node> path;
    public WalkabilityMask mask;

    public Rigidbody2D rb;

    public Animator anim;

    public float speed;

    public float nextNodeDistance;

    public Transform wallCheck;
    public float wallCheckDistance;
    public Transform groundCheck;
    public float groundCheckDistance;

    public bool alreadyJumped = false;
    public bool grounded = true;

    public float damage;

    public LayerMask ground;

    public float jumpTime;

    public int health;
    public int maxHealth;

    public GameObject deathParticle;

    public bool aiActive;

    public float stateTimeElapsed;

    //Shooter Squire
    public Vector3 currentTrackDirection;

    public GameObject target;

    public float attackDelay;

    public float trackTime;

    public float trackSpeed;

    public float detectionDistance;

    public LineRenderer line;

    public float lineDistance;

    void Start()
    {
        path = new List<Node>();
        if (poi.Count > 0)
        {
            path = pathfinding.FindPath(pathfinding.GetNearestNode(transform.position), poi[0], mask);
        }
    }

    void Update()
    {
        if (!aiActive)
            return;

        currentState.UpdateState(this);

        anim.SetBool("jumping", alreadyJumped);
    }

    public bool Countdown(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    public Vector3 GetDirection(Vector3 posA, Vector3 posB)
    {
        Vector3 direction = posA - posB;
        return direction.normalized;
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainInState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
}
