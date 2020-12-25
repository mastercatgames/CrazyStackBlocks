using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitController : MonoBehaviour
{
    public bool isMoving;
    public float moveSpeed;
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        if (isMoving && !gameController.isInGameOver)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isMoving = false;
        Invoke("CallSpawnNewBlock", 1f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        isMoving = true;
    }

    private void CallSpawnNewBlock()
    {
        if (!isMoving)
        {
            if (gameController.currentBlock == null && !gameController.isInGameOver)
            {
                bool moveCamera = false;
                moveCamera = transform.localPosition.y > gameController.bestHeight;   
                gameController.bestHeight = transform.localPosition.y;
                gameController.score.text = gameController.bestHeight.ToString("F1") + "m";
                gameController.SpawnNewBlock(moveCamera);
                //transform.localPosition = Vector3.zero; //Reset the limiter position to floor
                //Improved this code bellow, instead of always scan from the floor, scan close from the last height
                
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                transform.localPosition = new Vector3(0f, gameController.bestHeight - 1f, 0f);
            }
        }
    }
}
