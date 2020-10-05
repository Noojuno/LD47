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
    public static void Open()
    {
        if (Instance == null)
        {
            UIManager.Instance.CreateInstance<T>();
        }
        else
        {
            Instance.gameObject.SetActive(true);
        }

        UIManager.Instance.Open(Instance);
    }

    public static void Close()
    {
        if (Instance == null)
        {
            Debug.LogErrorFormat("Trying to close screen {0} but Instance is null", typeof(T));
            return;
        }

        UIManager.Instance.Close(Instance);
    }

    public override void OnEscapePressed()
    {
        base.OnEscapePressed();

        Close();
    }
}

public abstract class UIScreen : MonoBehaviour
{
    public virtual bool IsOverlay => false;
    public virtual bool DestroyWhenClosed => true;

    public virtual bool ShouldPause => false;

    public virtual void OnOpen()
    {

    }

    public virtual void OnClose()
    {

    }

    public virtual void OnEscapePressed()
    {
        
    }
}
