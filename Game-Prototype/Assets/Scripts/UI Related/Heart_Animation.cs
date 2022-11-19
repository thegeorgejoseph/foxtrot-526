using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_Animation : MonoBehaviour
{
    public Animator animator;

    public void heartLose()
    {
        animator.Play("Base Layer.Heart_Lost");
    }

    public void heartGain()
    {
        animator.Play("Base Layer.Heart_Gained");
    }


}
