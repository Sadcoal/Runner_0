using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;

    public float groundHeight;
    public float groundRight;
    public float screenRight;
    BoxCollider2D collider;

    bool didGenerateGround = false;
    public Coin coinTemplate;
    public Obstacle obstacleTemplate;
    public Wall wallTemplate;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        collider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider.size.y / 2);
        screenRight = Camera.main.transform.position.x * 2;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        groundRight = transform.position.x + (collider.size.x / 2);

        if (groundRight < 0)
        {
            Destroy(gameObject);
            return;
        }

        if (!didGenerateGround)
        {
            if (groundRight < screenRight)
            {
                didGenerateGround = true;
                generateGround();
            }
        }
        transform.position = pos;
    }

    void generateGround()
    {
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;

        float h1 = player.jumpVelocity * player.maxHoldJumpTime;
        float t = player.jumpVelocity / -player.gravity;
        float h2 = player.jumpVelocity * t + (0.5f * (player.gravity * (t * t)));
        float maxJumpHeight = h1 + h2;
        float maxY = maxJumpHeight * 0.8f;
        maxY += groundHeight;
        float minY = 2;
        float actualY = Random.Range(minY, maxY);

        pos.y = actualY - goCollider.size.y / 2;
        if (pos.y > 2.7f)
            pos.y = 2.7f;

        float t1 = t + player.maxHoldJumpTime;
        float t2 = Mathf.Sqrt((2.0f * (maxY - actualY)) / -player.gravity);
        float totalTime = t1 + t2;
        float maxX = totalTime * player.velocity.x;
        maxX *= 0.7f;
        maxX += groundRight - 7;
        float minX = screenRight + 2;
        float actualX = Random.Range(minX, maxX);

        pos.x = actualX + goCollider.size.x / 2;
        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();
        goGround.groundHeight = go.transform.position.y + (goCollider.size.y);

        int obstacleNum = Random.Range(0, 3);
        int coinNum = Random.Range(2, 3);
        int wallNum = Random.Range(1, 3);

        for (int i = 0; i < wallNum; i++)
        {
            float y = goGround.groundHeight + 30;
            float width = goCollider.size.x / 2 - 1;
            float left = go.transform.position.x - width + 7;
            float right = go.transform.position.x + width - 7;
            float x = Random.Range(left, right);
            GameObject wall = Instantiate(wallTemplate.gameObject);
            wall.transform.position = new Vector2(x, y);
        }
        for (int i = 0; i < obstacleNum; i++)
        {
            float y = goGround.groundHeight;
            float width = goCollider.size.x / 2 - 1;
            float left = go.transform.position.x - width + 10;
            float right = go.transform.position.x + width - 10;
            float x = Random.Range(left, right);
            GameObject obst = Instantiate(obstacleTemplate.gameObject);
            obst.transform.position = new Vector2(x, y);
        }
        for (int i = 0; i < coinNum; i++)
        {
            float y = maxJumpHeight - Random.Range(3, 5);
            float width = goCollider.size.x / 2 - 1;
            float left = go.transform.position.x - width;
            float right = go.transform.position.x + width;
            float x = Random.Range(left, right);
            GameObject coin = Instantiate(coinTemplate.gameObject);
            coin.transform.position = new Vector2(x, y);
        }
    }
}
