using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    private string currentState;
    const string BLINK = "blink";
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        ///stop the same animation from interrupting itself
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
    
    public void BackToIdle()
    {

    }
}
