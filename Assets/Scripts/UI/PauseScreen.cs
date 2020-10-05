using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : UIScreen<PauseMenu>
{
    public override void OnOpen()
    {
        base.OnOpen();

        GameManager.Instance.SetPaused(false);
    }

    public override void OnClose()
    {
        base.OnClose();

        GameManager.Instance.SetPaused(false);
    }
}
