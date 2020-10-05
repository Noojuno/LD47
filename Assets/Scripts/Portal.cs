using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != GameManager.Instance.Player.gameObject) return;

        GameManager.Instance.SetTimeRunning(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != GameManager.Instance.Player.gameObject) return;

        GameManager.Instance.SetTimeRunning(false);
    }
}
