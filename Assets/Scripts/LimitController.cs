using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitController : MonoBehaviour
{
    public bool isMoving;
    public float moveSpeed;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
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
            if (gameController.currentBlock == null)
            {
                print("Y: " + transform.localPosition.y);

                bool moveCamera = false;
                moveCamera = transform.localPosition.y > gameController.bestHeight;    

                gameController.bestHeight = transform.localPosition.y;
                gameController.score.text = gameController.bestHeight.ToString("F1");
                gameController.SpawnNewBlock(moveCamera);
                transform.localPosition = Vector3.zero;
            }
        }
    }
}
