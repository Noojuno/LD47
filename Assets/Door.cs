using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    public void Toggle(bool open)
    {
        Debug.Log(open);

        this.animator.SetBool("Open", open);
    }
}
