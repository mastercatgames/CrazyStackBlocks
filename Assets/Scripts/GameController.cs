using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] blocksList;
    public float bestHeight, speedModifier, timerAfterDrop, maxTimeAfterDrop;
    public bool isInGameOver;
    public GameObject currentBlock, lastBlockDropped, GameOverPanel;
    public Transform floor;
    public Text score;

    void Start()
    {
        timerAfterDrop = 6f; //after the max time
        maxTimeAfterDrop = 5f;
        speedModifier = 0.005f;
        SpawnNewBlock(false);
    }

    public void SpawnNewBlock(bool moveCamera)
    {
        if (moveCamera)
        {
            //TODO: Smooth moviment
            transform.position = new Vector3(0, bestHeight, 0);

            Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y + 0.5f, -10f);

            if (Camera.main.transform.position.y > 2.5f)
            {
                Camera.main.orthographicSize += 0.7f;
            }
        }
        // Instantiate at position (0, 0, 0) and zero rotation.
        currentBlock = Instantiate(blocksList[Random.Range(0, blocksList.Length)], new Vector3(0, transform.position.y, 0), Quaternion.identity);
    }

    public void DropBlock()
    {
        // if (timerAfterDrop > 6f)
        // {
        currentBlock.transform.Find("Arrows").gameObject.SetActive(false);
        currentBlock.transform.SetParent(GameObject.Find("Stack").transform);
        currentBlock.GetComponentInChildren<Block>().isGrabbing = false;
        currentBlock.GetComponentInChildren<Block>().wasDropped = true;
        currentBlock.GetComponentInChildren<Block>().look = true;

        Transform eyes = currentBlock.transform.Find("Block").Find("Face").Find("Eyes");

        eyes.Find("_rightEye").Find("Eye").Find("Pupil").localPosition = new Vector3(0f, 0.211f, 0f);
        eyes.Find("_leftEye").Find("Eye").Find("Pupil").localPosition = new Vector3(0f, 0.211f, 0f);

        currentBlock.GetComponentInChildren<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        lastBlockDropped = currentBlock;
        currentBlock = null;
        timerAfterDrop = 0f;
        // }
    }

    private void Update()
    {
        if (!isInGameOver)
        {
            if (Input.touchCount > 0 && currentBlock != null)
            {
                Touch touch = Input.GetTouch(0);
                currentBlock.GetComponentInChildren<Block>().isGrabbing = true;
                currentBlock.transform.Find("Arrows").gameObject.SetActive(true);

                if (touch.phase == TouchPhase.Moved)
                {
                    currentBlock.transform.position = new Vector3(currentBlock.transform.position.x + touch.deltaPosition.x * speedModifier, currentBlock.transform.position.y, 0);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    currentBlock.GetComponentInChildren<Rigidbody2D>().velocity = Vector2.zero;
                    //print("DropBlock!");
                    DropBlock();
                }
            }

            if (timerAfterDrop < maxTimeAfterDrop)
            {
                timerAfterDrop += Time.deltaTime;
            }
            else if (lastBlockDropped != null)
            {
                //if time is over and the current form that was dropped still static
                if (!lastBlockDropped.GetComponentInChildren<Block>().isStatic)
                {
                    lastBlockDropped.GetComponentInChildren<Block>().SleepBlock();
                }
            }
        }
    }

    public void GameOver()
    {
        isInGameOver = true;
        score.gameObject.SetActive(false);

        GameOverPanel.transform.Find("Title").GetComponentInChildren<Text>().text = "Score: " + score.text;
        GameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
