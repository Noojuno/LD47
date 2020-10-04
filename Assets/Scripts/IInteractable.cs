using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

interface IInteractable
{
    void OnInteract(PlayerController interactor);

    void OnSelect(PlayerController selector);

    void OnDeselect(PlayerController selector);
}
