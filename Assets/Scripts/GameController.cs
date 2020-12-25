﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] blocksList;
    public float bestHeight, speedModifier;
    public GameObject currentBlock;
    public Transform floor;    
    public Text score;    

    void Start()
    {
        speedModifier = 0.005f;
        SpawnNewBlock(false);
    }

    public void SpawnNewBlock(bool moveCamera)
    {
        if (moveCamera)
        {
            //TODO: Smooth moviment
            transform.position = new Vector3(0, bestHeight - 0.5f, 0);

            Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y + 0.5f, -10);

            if (Camera.main.transform.position.y > 2.5)
            {
                Camera.main.orthographicSize += 0.7f;
            }
        }
        // Instantiate at position (0, 0, 0) and zero rotation.
        currentBlock = Instantiate(blocksList[Random.Range(0, blocksList.Length)], new Vector3(0, transform.position.y, 0), Quaternion.identity);
    }

    public void DropBlock()
    {
        currentBlock.transform.Find("Arrows").gameObject.SetActive(false);
        currentBlock.transform.SetParent(GameObject.Find("Stack").transform);
        currentBlock.GetComponentInChildren<Block>().isGrabbing = false;
        currentBlock.GetComponentInChildren<Block>().wasDropped = true;
        currentBlock.GetComponentInChildren<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        currentBlock = null;
    }

    private void Update()
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
                print("DropBlock!");
                DropBlock();
            }
        }
    }
}
