using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
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
        pos.y -= (player.velocity.x * Time.fixedDeltaTime) / 9;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        if (pos.x < -100)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}