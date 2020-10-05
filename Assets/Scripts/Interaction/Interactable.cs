using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    public UnityEvent<PlayerController> OnInteractEvent;

    public virtual void Start()
    {
        if (this.OnInteractEvent == null)
        {
            this.OnInteractEvent = new UnityEvent<PlayerController>();
        }
    }

    public virtual void OnInteract(PlayerController player)
    {
        this.OnInteractEvent.Invoke(player);
    }

    public virtual void OnSelect(PlayerController player)
    {
        foreach (var spriteRenderer in this.spriteRenderers)
        {
            spriteRenderer.material.SetColor("_Tint", new Color(1, 1, 1, 0.3f));
        }
    }

    public virtual void OnDeselect(PlayerController player)
    {
        foreach (var spriteRenderer in this.spriteRenderers)
        {
            spriteRenderer.material.SetColor("_Tint", new Color(0, 0, 0, 0));
        }
    }
}
