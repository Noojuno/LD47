using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Holdable : Interactable
{
    public override void OnInteract(PlayerController player)
    {
        base.OnInteract(player);

        if (player.holding == this.gameObject)
        {
            player.Hold(null);
        }
        else
        {
            player.Hold(this.gameObject);
        }
    }
}
