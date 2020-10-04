using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public SpriteRenderer spriteRenderer;

    public virtual void OnInteract(PlayerController interactor)
    {
        Debug.Log("Interactable go brrrrrrrrrrrr");
    }

    public virtual void OnSelect(PlayerController selector)
    {
        this.spriteRenderer.material.SetColor("_Tint", new Color(1, 1, 1, 0.3f));
    }

    public virtual void OnDeselect(PlayerController selector)
    {
        this.spriteRenderer.material.SetColor("_Tint", new Color(0, 0, 0, 0));
    }
}
