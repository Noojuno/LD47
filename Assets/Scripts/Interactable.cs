using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public SpriteRenderer spriteRenderer;

    private Color initialColor;

    void Awake()
    {
        this.initialColor = spriteRenderer.color;
    }

    public virtual void OnInteract(PlayerController interactor)
    {
        Debug.Log("Interactable go brrrrrrrrrrrr");
    }

    public virtual void OnSelect(PlayerController selector)
    {
        this.spriteRenderer.color = Color.red;
    }

    public virtual void OnDeselect(PlayerController selector)
    {
        this.spriteRenderer.color = this.initialColor;
    }
}
