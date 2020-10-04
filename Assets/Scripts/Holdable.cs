using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Holdable : Interactable
{
    public override void OnInteract(PlayerController interactor)
    {
        if (interactor.holding == this.gameObject)
        {
            interactor.Hold(null);
        }
        else
        {
            interactor.Hold(this.gameObject);
        }
    }
}
