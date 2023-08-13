using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersController : MonoBehaviour
{
    public Animator animator;
    private bool open;

   
    public void ClickParameters()
    {
        open = animator.GetBool("Open");
        animator.SetBool("Open", !open);
    }
}
