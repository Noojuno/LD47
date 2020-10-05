using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public sealed class UIManager : Singleton<UIManager>
{
    public Stack<UIScreen> screenStack = new Stack<UIScreen>();
    public UIScreen CurrentScreen => screenStack.Count > 0 ? screenStack.Peek() : null;

    public List<UIScreen> Prefabs = new List<UIScreen>();

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.CurrentScreen?.OnEscapePressed();
        }
    }

    public void CreateInstance<T>() where T : UIScreen
    {
        var prefab = GetPrefab<T>();

        Instantiate(prefab, transform);
    }

    private T GetPrefab<T>() where T : UIScreen
    {
        foreach (var prefab in this.Prefabs)
        {
            if (prefab.GetType() == typeof(T))
            {
                return (T)prefab;
            }
        }

        throw new MissingReferenceException("Prefab not found for type " + typeof(T));
    }

    public void Open(UIScreen instance)
    {
        if (this.CurrentScreen == instance) return;

        if (!instance.IsOverlay)
        {
            foreach (var screen in screenStack)
            {
                screen.gameObject.SetActive(false);

                if (!screen.IsOverlay)
                    break;
            }
        }

        if (instance.ShouldPause)
        {
            GameManager.Instance.SetPaused(true);
        }

        if (this.CurrentScreen != null)
        {
            var topCanvas = instance.GetComponent<Canvas>();
            var previousCanvas = this.CurrentScreen.GetComponent<Canvas>();
            topCanvas.sortingOrder = previousCanvas.sortingOrder + 1;
        }

        instance.OnOpen();

        this.screenStack.Push(instance);
    }

    public void Close(UIScreen screen)
    {
        if (screenStack.Count == 0)
        {
            Debug.LogErrorFormat(screen, "{0} cannot be closed because screen stack is empty", screen.GetType());
            return;
        }

        if (screenStack.Peek() != screen)
        {
            Debug.LogErrorFormat(screen, "{0} cannot be closed because it is not on top of stack", screen.GetType());
            return;
        }

        CloseTopUIScreen();
    }

    public void CloseTopUIScreen()
    {
        var instance = screenStack.Pop();

        instance.OnClose();

        if (instance.DestroyWhenClosed)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance.gameObject.SetActive(false);
        }

        // Re-activate top screen
        // If a re-activated screen is an overlay we need to activate the screen under it
        foreach (var screen in screenStack)
        {
            screen.gameObject.SetActive(true);

            if (!screen.IsOverlay)
                break;
        }

        bool shouldPause = false;
        foreach (var screen in screenStack)
        {
            if (screen.ShouldPause)
            {
                shouldPause = true;
                break;
            }
        }

        GameManager.Instance.SetPaused(shouldPause);
    }
}