using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    void SetVisible(bool visible)
    {
        if (GameManager.Instance.Player != null) GameManager.Instance.Player.movementEnabled = !visible;
        GameManager.Instance.runTimer = false;
    }
}
