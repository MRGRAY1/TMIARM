using UnityEngine.UIElements;

public abstract class UIView
{
    protected VisualElement root;

    protected UIView(VisualElement rootElement)
    {
        root = rootElement;
        RegisterCallbacks();
    }

    protected abstract void RegisterCallbacks();

    public virtual void Show()
    {
        root.style.display = DisplayStyle.Flex;
    }

    public virtual void Hide()
    {
        root.style.display = DisplayStyle.None;
    }

    public virtual void Dispose()
    {
        UnregisterCallbacks();
    }

    protected virtual void UnregisterCallbacks() { }
}