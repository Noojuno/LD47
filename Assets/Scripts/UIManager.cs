using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public sealed class UIManager : Singleton<UIManager>
{
    public Stack<UIScreen> screenStack = new Stack<UIScreen>();
    public UIScreen CurrentScreen => screenStack.Count > 0 ? screenStack.Peek() : null;

    public PauseMenu PauseMenuPrefab;
    public HUDScreen HUDScreenPrefab;

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
        // Get prefab dynamically, based on public fields set from Unity
        // You can use private fields with SerializeField attribute too
        var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (var field in fields)
        {
            var prefab = field.GetValue(this) as T;
            if (prefab != null)
            {
                return prefab;
            }
        }

        throw new MissingReferenceException("Prefab not found for type " + typeof(T));
    }

    public void Open(UIScreen instance)
    {
        if (!instance.IsOverlay)
        {
            foreach (var screen in screenStack)
            {
                screen.gameObject.SetActive(false);

                if (!screen.IsOverlay)
                    break;
            }
        }

        var topCanvas = instance.GetComponent<Canvas>();
        var previousCanvas = this.CurrentScreen.GetComponent<Canvas>();
        topCanvas.sortingOrder = previousCanvas.sortingOrder + 1;

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
    }
}