using UnityEngine.UIElements;

public abstract class UIView
{
    protected VisualElement root;
    public VisualElement Root => root;

    protected UIView(VisualElement rootElement)
    {
        root = rootElement;
        RegisterCallbacks();
        Hide();
    }

    protected abstract void RegisterCallbacks();
    protected abstract void UnregisterCallbacks();

    public virtual void Show()
    {
        root.visible = true;
        root.pickingMode = PickingMode.Position;
    }

    public virtual void Hide()
    {
        root.visible = false;
        root.pickingMode = PickingMode.Ignore;
    }

    public virtual void Dispose()
    {
        UnregisterCallbacks();
    }
}