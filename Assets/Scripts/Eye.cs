using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    private GameController gameController;
    public Vector3 target;
    void Start()
    {
        if (GameObject.Find("GameController"))
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }
        target = Vector3.zero;
        InvokeRepeating("BlinkEyes", 1f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameController") && transform.parent.GetComponentInParent<Block>().look)
        {
            if (gameController.currentBlock != null)
            {
                target = gameController.currentBlock.transform.Find("Block").position;
            }
            else if (gameController.lastBlockDropped != null)
            {
                target = gameController.lastBlockDropped.transform.Find("Block").position;
            }

            if (gameController.currentBlock != null || gameController.lastBlockDropped != null)
            {
                Vector2 direction = new Vector2(
                target.x - transform.position.x,
                target.y - transform.position.y
                );

                transform.up = direction;
            }
        }
    }

    void BlinkEyes()
    {
        transform.parent.GetComponentInParent<Animator>().Play("Blink");
    }
}
