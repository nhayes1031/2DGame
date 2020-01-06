using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    private Transform playerTransform;
    private PlayerAimController playerAimController;
    private PlayerPlatformerController playerPlatformerController;
    [SerializeField] private Sprite successfulHitSprite;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;

    [SerializeField] private float maxTravelDistance = 5f;
    [SerializeField] private float returnAcceleration = 0.5f;
    [SerializeField] private float speed = 5f;

    private Vector2 originPoint;
    private bool isReturning = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject player = GameObject.Find("Player");
        playerTransform = player.transform;
        playerAimController = player.GetComponent<PlayerAimController>();
        playerPlatformerController = player.GetComponent<PlayerPlatformerController>();
        rb2d = GetComponent<Rigidbody2D>();
        originPoint = transform.position;
    }

    private void Start()
    {
        Vector2 move = transform.right * speed * Time.deltaTime;
        rb2d.AddForce(move);
    }

    private void Update()
    {
        if (isReturning)
        {
            Vector2 move = (playerTransform.position - transform.position).normalized * speed * 100 * Time.deltaTime;
            rb2d.velocity = move;
            speed += returnAcceleration;

            if (Mathf.Round(transform.position.x) == Mathf.Round(playerTransform.position.x) && Mathf.Round(transform.position.y) == Mathf.Round(playerTransform.position.y))
            {
                playerAimController.isFiring = false;
                Destroy(gameObject);
            }
        }

        if (!isReturning && Vector2.Distance(transform.position, originPoint) > maxTravelDistance)
        {
            isReturning = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            spriteRenderer.sprite = successfulHitSprite;
            playerPlatformerController.EnableDoubleJump();
        }
        isReturning = true;
    }
}
