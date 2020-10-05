using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : UIScreen<PauseScreen>
{
    public override bool ShouldPause => true;

    public override bool IsOverlay => true;

    public void OpenControls()
    {
        ControlsScreen.Open();
    }
}
