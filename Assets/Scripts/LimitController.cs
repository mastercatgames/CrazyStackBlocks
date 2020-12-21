using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitController : MonoBehaviour
{
    //public bool hasMore;
    public bool isMoving;
    //public bool isSpawningNewBlock;
    public Vector3 target;
    public float moveSpeed;
    public float currentYPosition;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(0f, -5f, 0);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        currentYPosition = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isMoving)
        {
            //transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.transform.parent.name == "Stack" /*&& isMoving*/)
    //     {
    //         isMoving = false;

    //         //Invoke("ResetLimitPosition", 0.5f);
    //         print(Mathf.RoundToInt(transform.position.y + 1.6f));
    //         if (Mathf.RoundToInt(transform.position.y + 1.6f) > gameController.bestHeight)
    //         {
    //             gameController.bestHeight = Mathf.RoundToInt(transform.position.y + 1.6f);
    //         }
    //         ResetLimitPosition();

    //         gameController.SpawnNewBlock(true);
    //         //transform.position = new Vector3(0, 5f, 0);
    //         //print(transform.position);
    //     }
    //     //gameObject.SetActive(false);        
    // }

    void OnTriggerExit2D(Collider2D other)
    {
        //print("OnTriggerExit2D");
        isMoving = false;
        //        gameController.bestHeight = transform.position.y;

        // if (isMoving)
        // {
        //     print("Está movendo ainda!!!");
        // }
        Invoke("CallSpawnNewBlock", 1f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        isMoving = true;
    }

    private void ResetLimitPosition()
    {
        //reset position to current
        transform.position = new Vector3(0, currentYPosition, 0);

        if (Camera.main.transform.position.y > 2.5)
        {
            transform.position = new Vector3(0, currentYPosition + 2f, 0);
        }
        else
        {
            transform.position = new Vector3(0, currentYPosition + 0.8f, 0);
        }

        currentYPosition = transform.position.y;
    }

    private void CallSpawnNewBlock()
    {
        if (isMoving)
        {
            print("Está movendo ainda!!!");
        }
        else
        {
            print(">> Parou de mover!!!");

            print(gameController.currentBlock);

            if (gameController.currentBlock == null)
            {
                gameController.bestHeight = transform.localPosition.y;//.ToString("F1");
                gameController.score.text = gameController.bestHeight.ToString("F1");
                //isSpawningNewBlock = true;
                gameController.SpawnNewBlock(true);

                transform.localPosition = Vector3.zero;

                //Invoke("ResetIsSpawningNewBlock", 2f);
            }
        }

        // if (!isMoving)
        // {
        //     gameController.SpawnNewBlock(true);            
        // }
    }

    // void ResetIsSpawningNewBlock()
    // {
    //     isSpawningNewBlock = false;
    // }
}
