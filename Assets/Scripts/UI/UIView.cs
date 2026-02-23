using UnityEngine.UIElements;

public abstract class UIView
{
    protected VisualElement root;

    protected UIView(VisualElement rootElement)
    {
        root = rootElement;
        RegisterCallbacks();
        Hide(); // make sure hidden on creation
    }

    // Called by child classes to setup button/event callbacks
    protected abstract void RegisterCallbacks();
    protected abstract void UnregisterCallbacks();
    public abstract void ToggleDebugOverlay(object obj);

    // Show the view
    public virtual void Show()
    {
        root.style.display = DisplayStyle.Flex;
        root.pickingMode = PickingMode.Position;  // <<< important for clicks
    }

    // Hide the view
    public virtual void Hide()
    {
        root.style.display = DisplayStyle.None;
        root.pickingMode = PickingMode.Ignore;    // <<< prevent hidden elements from intercepting clicks
    }

    // Dispose pattern for unregistering callbacks
    public virtual void Dispose()
    {
        UnregisterCallbacks();
    }

}