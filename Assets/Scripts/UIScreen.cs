using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen<T> : UIScreen where T : UIScreen<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        Instance = (T)null;
    }
}

public abstract class UIScreen : MonoBehaviour
{
    public bool IsOverlay => false;
    public bool DestroyWhenClosed => true;

    public void Open()
    {
        this.OnOpen();
    }

    public virtual void OnOpen()
    {

    }

    public void Close()
    {
        this.OnClose();
    }

    public virtual void OnClose()
    {

    }

    public virtual void OnEscapePressed()
    {
        this.Close();
    }
}
