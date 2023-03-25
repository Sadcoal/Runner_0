using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public bool isHoldingSlide = false;
    public bool isDead = false;
    public Vector2 velocity;
    public float gravity = 400;
    public float distance = 0;
    public float coins = 0;
    public float maxXVelocity = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float jumpVelocity = 20;
    //private float slideVelocity;
    public float groundHeight = 10;
    public float holdJumpTime = 0.35f;
    public float maxHoldJumpTime = 0.4f;
    public float maxHoldSlideTime = 0.5f;
    public float holdJumpTimer = 0.0f;
    public float holdSlideTimer = 0.0f;
    public float treshold = 2;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        if(isGrounded || groundDistance <= treshold)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //slideVelocity = velocity.x;
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
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        distance += velocity.x * Time.fixedDeltaTime;

        if (isDead)
        {
            return;
        }

        if (pos.y < -1)
        {
            isDead = true;
        }

        if (isHoldingJump)
        {
            holdJumpTimer += Time.fixedDeltaTime;

            if (holdJumpTimer >= holdJumpTime || Input.GetKeyUp(KeyCode.Space))
            {
                isHoldingJump = false;
            }
        }

        if(isHoldingSlide)
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
            Vector2 rayOrigin = new Vector2(pos.x + 0.3f, pos.y);
            Vector2 rayDirection = Vector2.up;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            if (hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();

                if (ground != null)
                {
                    groundHeight = ground.groundHeight;
                    pos.y = groundHeight;
                    isGrounded = true;
                }
            }

            Debug.DrawRay (rayOrigin, rayDirection * rayDistance, Color.red);

            

        }

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            Vector2 rayOrigin = new Vector2(pos.x - 0.3f, pos.y);
            Vector2 rayDirection = Vector2.up;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            acceleration = maxAcceleration * (1 - velocityRatio);
            holdJumpTime = maxHoldJumpTime * velocityRatio;
            velocity.x += acceleration * Time.fixedDeltaTime;
            
            if(isHoldingSlide)
            {
                velocity.x += maxAcceleration;
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

        Vector2 obstRayOrigin = new Vector2(pos.x, pos.y);
        Vector2 coinRayOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D obstForwardHit = Physics2D.Raycast(obstRayOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
        RaycastHit2D coinForwardHit = Physics2D.Raycast(coinRayOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);

        if (obstForwardHit.collider != null || coinForwardHit.collider != null)
        {
            Obstacle obstacle = obstForwardHit.collider.GetComponent<Obstacle>();
            Coin coin = coinForwardHit.collider.GetComponent<Coin>();

            if (obstacle != null)
            {
                hitObstacle(obstacle);
            }

            if (coin != null)
            {
                hitCoin(coin);
            }
        }

        RaycastHit2D obstDownHit = Physics2D.Raycast(obstRayOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime);
        RaycastHit2D coinDownHit = Physics2D.Raycast(coinRayOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime);

        if (obstDownHit.collider != null || coinDownHit.collider != null)
        {
            Obstacle obstacle = obstDownHit.collider.GetComponent<Obstacle>();
            Coin coin = coinDownHit.collider.GetComponent<Coin>();

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

    void hitObstacle(Obstacle obst)
    {
        Destroy(obst.gameObject);
        velocity.x *= 0.7f;
    }

    void hitCoin(Coin coin)
    {
        Destroy(coin.gameObject);
        coins ++;
    }
}
