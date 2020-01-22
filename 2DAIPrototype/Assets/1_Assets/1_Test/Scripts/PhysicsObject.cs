using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Questions?
    // Why use for loops
    // Why do we have hitBuffer and hitBufferList?

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    public Vector2 targetVelocity;
    private bool grounded;
    protected Vector2 groundNormal;
    protected Vector2 velocity;
    protected Rigidbody2D rb2D;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float shellRadius = 0.01f;
    protected const float minMoveDistance = 0.001f;

    protected delegate void GroundedChanged();
    protected GroundedChanged groundedChanged;

    public bool Grounded { 
        get => grounded;
        set
        {
            if (grounded != value)
            {
                grounded = value;
                groundedChanged();
            } else
                grounded = value;
        }
    }

    private void OnEnable()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    private void Start()
    {
        contactFilter.useLayerMask = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        Grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    private void Movement(Vector2 move, bool yMove)
    {
        float distance = move.magnitude;
        if (distance > minMoveDistance)
        {
            int count = rb2D.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    Grounded = true;
                    if (yMove)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }

            rb2D.position = rb2D.position + move.normalized * distance;
        }
    }

    protected virtual void ComputeVelocity()
    {

    }
}
