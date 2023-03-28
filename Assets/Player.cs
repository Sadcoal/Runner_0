using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public bool isHoldingSlide = false;
    public bool isDead = false;
    public bool ignoreHit = false;
    public Vector2 velocity;
    public float gravity = 400;
    public float distance = 0;
    public float coins = 5;
    public float maxXVelocity = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public float holdJumpTime = 0.35f;
    public float maxHoldJumpTime = 0.4f;
    public float maxHoldSlideTime = 0.5f;
    public float maxIgnoreHitTime = 2.5f;
    public float ignoreHitTimer = 0.0f;
    public float holdJumpTimer = 0.0f;
    public float holdSlideTimer = 0.0f;
    public float treshold = 1;

    void Start()
    {

    }

    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        if (isGrounded || groundDistance <= treshold)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    isHoldingJump = false;
                }
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                isHoldingSlide = true;
                holdSlideTimer = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            isHoldingJump = false;
        }
        else
        {
            isHoldingSlide = false;
        }

        if (Input.GetKeyDown(KeyCode.CapsLock) && coins >= 5)
        {
            ignoreHit = true;
            coins -= 5;
            ignoreHitTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (ignoreHit)
        {
            ignoreHitTimer += Time.fixedDeltaTime;
            if (ignoreHitTimer >= maxIgnoreHitTime)
            {
                ignoreHit = false;
            }
        }

        if (isDead)
        {
            velocity.x = 0;
            return;
        }
 
        distance += velocity.x * Time.fixedDeltaTime;

        if (isHoldingJump)
        {
            holdJumpTimer += Time.fixedDeltaTime;

            if (holdJumpTimer >= holdJumpTime || Input.GetKeyUp(KeyCode.Space))
            {
                isHoldingJump = false;
            }
        }

        if (isHoldingSlide)
        {
            holdSlideTimer += Time.fixedDeltaTime;

            if (holdSlideTimer >= maxHoldSlideTime || isHoldingJump || Input.GetKeyUp(KeyCode.LeftShift) || !isGrounded)
            {
                isHoldingSlide = false;
                velocity.x -= 75;
            }
        }

        if (!isGrounded)
        {
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            pos.y += velocity.y * Time.fixedDeltaTime;
            Vector2 rayOrigin = new Vector2(pos.x + 0.5f, pos.y);
            Vector2 rayDirection = Vector2.up;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            if (hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                Edge edge = hit2D.collider.GetComponent<Edge>();
                Wall wall = hit2D.collider.GetComponent<Wall>();
                Coin coin = hit2D.collider.GetComponent<Coin>();

                if (edge != null || wall != null)
                {
                    isDead = true;
                }

                if (coin != null)
                {
                    hitCoin(coin);
                }

                if (ground != null)
                {
                    groundHeight = ground.groundHeight;
                    pos.y = groundHeight;
                    isGrounded = true;
                }

            }

            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
        }

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            Vector2 rayOrigin = new Vector2(pos.x - 0.5f, pos.y);
            Vector2 rayDirection = Vector2.up;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            acceleration = maxAcceleration * (1 - velocityRatio);
            holdJumpTime = maxHoldJumpTime * velocityRatio;
            velocity.x += acceleration * Time.fixedDeltaTime;

            if (isHoldingSlide)
            {
                velocity.x += 75;
            }

            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }

            if (hit2D.collider == null)
            {
                isGrounded = false;
            }

            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        Vector2 rayOrigin2 = new Vector2(pos.x, pos.y);

        RaycastHit2D forwardHit = Physics2D.Raycast(rayOrigin2, Vector2.right, velocity.x * Time.fixedDeltaTime);
        
        if (forwardHit.collider != null)
        {
            Obstacle obstacle = forwardHit.collider.GetComponent<Obstacle>();
            Coin coin = forwardHit.collider.GetComponent<Coin>();
            Wall wall = forwardHit.collider.GetComponent<Wall>();

            if (wall != null)
            {
                hitWall(wall);
            }

            if (obstacle != null)
            {
                hitObstacle(obstacle);
            }

            if (coin != null)
            {
                hitCoin(coin);
            }

        }

        RaycastHit2D downHit = Physics2D.Raycast(rayOrigin2, Vector2.up, velocity.y * Time.fixedDeltaTime);
       
        if (downHit.collider != null)
        {
            Obstacle obstacle = downHit.collider.GetComponent<Obstacle>();
            Coin coin = downHit.collider.GetComponent<Coin>();

            if (obstacle != null)
            {
                hitObstacle(obstacle);
            }

            if (coin != null)
            {
                hitCoin(coin);
            }
        }

        transform.position = pos;
    }

    void hitWall(Wall wall)
    {
        if (!ignoreHit)
        {
            Destroy(wall.gameObject);
            isDead = true;
        }
        else
        {
            Destroy(wall.gameObject);
            isDead = false;
        }
    }

    void hitObstacle(Obstacle obst)
    {
        if (!ignoreHit)
        {
            Destroy(obst.gameObject);
            isDead = true;
        }
        else
        {
            Destroy(obst.gameObject);
            isDead = false;
        }
    }

    void hitCoin(Coin coin)
    {
        Destroy(coin.gameObject);
        coins++;
    }
}