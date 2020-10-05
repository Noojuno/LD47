using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogData dialog;
    public bool oneTime = true;
    private bool hasRun = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (dialog == null) return;
        if (oneTime && hasRun) return;
        if (collider.isTrigger) return;
        if (collider.gameObject != GameManager.Instance.Player?.gameObject) return;

        DialogScreen.Open(dialog);

        this.hasRun = true;
    }
}
