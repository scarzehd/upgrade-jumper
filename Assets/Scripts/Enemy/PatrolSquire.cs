using System.Collections.Generic;
using UnityEngine;

public class PatrolSquire : MonoBehaviour
{
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

    public int damage;

    public LayerMask ground;

    public float jumpTime;

    [SerializeField] private GameObject deathParticle;

    void Start()
    {
        path = new List<Node>();
        if (poi.Count > 0)
        {
            path = pathfinding.FindPath(pathfinding.GetNearestNode(transform.position), poi[0], mask);
        }

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        Health health = GetComponent<Health>();
        health.onDeath += OnDeath;
    }

    void OnDeath()
    {
        Instantiate(deathParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Destroy(gameObject);
    }

    void Update()
    {
        if (poi.Count > 0)
        {
            Move();
        }
        UpdateAnimState();
    }

    void Move()
    {
        if (Vector2.Distance(transform.position, path[path.Count - 1].transform.position) <= nextNodeDistance)
        {
            //cycle through poi's
            Node node1 = poi[0];
            poi.RemoveAt(0);
            poi.Add(node1);
            path = pathfinding.FindPath(pathfinding.GetNearestNode(transform.position), poi[0], mask);
        }

        if (Vector2.Distance(transform.position, path[0].transform.position) <= nextNodeDistance)
        {
            //remove waypoints we've already reached
            path.RemoveAt(0);
            if (path[0].transform.position.y - transform.position.y > 1)
            {
                StartCoroutine(Jump(path[0].transform.position));
                alreadyJumped = true;
            }
        }

        //check if we're on the ground
        grounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, ground);

        //move and turn
        if (path[0].transform.position.x > transform.position.x)
        {
            if (grounded == true)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
        else
        {
            if (grounded == true)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }

        if (grounded == true)
        {
            //if we're on the ground, reset this so we can jump
            alreadyJumped = false;
        }
        else if (grounded == false && alreadyJumped == false)
        {
            //jump when we're off the ground, but only if we haven't jumped yet
            alreadyJumped = true;
            StartCoroutine(Jump(path[0].transform.position));
        }
    }

    void UpdateAnimState()
    {
        anim.SetBool("jumping", alreadyJumped);
    }

    System.Collections.IEnumerator Jump(Vector3 target, float jumpAngle = 65)
    {

        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        yield return new WaitForSeconds(jumpTime);

        float gravity = rb.gravityScale * Physics2D.gravity.magnitude;

        //calculate angle in radians
        float angle = jumpAngle * Mathf.Deg2Rad;

        //remove z components of positions
        Vector3 planarTarget = new Vector3(target.x, target.y, 0);
        Vector3 planarPosition = new Vector3(transform.position.x, transform.position.y, 0);

        //distance between us and the target
        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = transform.position.y - target.y;

        //get initial velocity
        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(initialVelocity * Mathf.Cos(angle), initialVelocity * Mathf.Sin(angle), 0);

        //rotate velocity to match the direction between us and the target
        float angleBetweenObjects = Vector3.Angle(Vector2.right, planarTarget - planarPosition);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector2.up) * velocity;

        //jump!
        rb.velocity = finalVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Player"))
        {
            Health playerHealth = go.GetComponent<Health>();
            PlayerMovement playerMovement = go.GetComponent<PlayerMovement>();
            if (transform.position.x < go.transform.position.x)
            {
                playerMovement.knockbackRight = true;
            } else
            {
                playerMovement.knockbackRight = false;
            }
            playerHealth.Damage(damage);
        }
    }
}
