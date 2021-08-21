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

    void Update()
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
            if (gameController.currentBlock == null && !gameController.isInGameOver  && gameController.timerAfterDrop >= 6f)
            {
                bool moveCamera = false;
                moveCamera = Mathf.RoundToInt(transform.localPosition.y) > Mathf.RoundToInt(gameController.bestHeight);
                gameController.bestHeight = transform.localPosition.y;//float.Parse(transform.localPosition.y.ToString("F1"));
                gameController.score.text = gameController.bestHeight.ToString("F1") + "m";
                gameController.SpawnNewBlock(moveCamera);
                //transform.localPosition = Vector3.zero; //Reset the limiter position to floor
                //Improved this code bellow, instead of always scan from the floor, scan close from the last height

                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                //transform.localPosition = new Vector3(0f, gameController.bestHeight - 1f, 0f);
                transform.localPosition = Vector3.zero;

                if (moveCamera)
                {
                    moveSpeed += (gameController.bestHeight * 0.1f);
                }
            }
        }
    }

    public void Move()
    {
        if (isMoving == false)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            isMoving = true;
        }
    }
}
