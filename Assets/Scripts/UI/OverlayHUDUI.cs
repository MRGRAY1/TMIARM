using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Collections.Unicode;

public class OverlayHUDUI : UIView
{
    private VisualElement debugContainer;
    private DebugOverlayHUDUI debugOverlayHUD;

    public OverlayHUDUI(VisualElement rootElement) : base(rootElement)
    {
        debugContainer = root.Q<VisualElement>("DebugDocument");
        debugOverlayHUD = new DebugOverlayHUDUI(debugContainer);

    }

    protected override void RegisterCallbacks()
    {
        
    }

    protected override void UnregisterCallbacks()
    {
        
    }
    // Show a transient notification inside the overlay HUD that slides down from the top,
    // stays for 5 seconds, then slides back up.
    // - text: message to show
    // - displayDuration: how long the notification stays visible (seconds)
    // - slideDuration: how long the slide animation takes (seconds)
    // - targetTopPx: how many pixels from the top the notification should rest at
    public void ShowNotificationSliding(string text, float displayDuration = 5f, float slideDuration = 0.5f, float targetTopPx = 100f)
    {
        if (string.IsNullOrEmpty(text)) return;


        // Find the container defined in your UXML (recommended). If it's missing, create one.
        var container = root.Q<VisualElement>("NotificationContainer");
        if (container == null)
        {
            container = new VisualElement { name = "NotificationContainer" };
            container.style.position = Position.Absolute;
            container.style.left = new StyleLength(new Length(50, LengthUnit.Percent));
            // shift left by half width will happen by setting margin-left after width is known or via USS
            container.style.width = 750;
            container.style.height = 100;
            container.style.alignItems = Align.Center;
            container.style.justifyContent = Justify.Center;
            root.Add(container);
        }

        // Ensure there's a label inside the container to show text.
        var label = container.Q<Label>("NotificationLbl");
        if (label == null)
        {
            label = new Label { name = "NotificationLbl" };
            label.AddToClassList("notification");
            label.style.unityTextAlign = TextAnchor.MiddleCenter;
            label.style.fontSize = 24;
            label.style.paddingLeft = 8;
            label.style.paddingRight = 8;
            label.style.paddingTop = 6;
            label.style.paddingBottom = 6;
            label.style.backgroundColor = new StyleColor(new Color(0f, 0f, 0f, 0.75f));
            label.style.color = new StyleColor(Color.white);
            label.style.borderTopLeftRadius = 4;
            label.style.borderTopRightRadius = 4;
            label.style.borderBottomLeftRadius = 4;
            label.style.borderBottomRightRadius = 4;
            container.Add(label);
        }

        label.text = text;
        container.style.display = DisplayStyle.Flex;

        // Start coroutine which animates container.top from off-screen to targetTopPx, waits, then back up.
        CoroutineRunner.StartCoroutine(this.ShowNotificationCoroutine(container, displayDuration, slideDuration, targetTopPx));
    }

    private IEnumerator ShowNotificationCoroutine(VisualElement container, float displayDuration, float slideDuration, float targetTopPx)
    {
        // Determine height (resolvedStyle may be zero first frame; fallback to 100)
        float height = container.resolvedStyle.height;
        if (height <= 0f) height = container.style.height.value.value > 0 ? container.style.height.value.value : 100f;

        // Place container centered horizontally (left 50% minus half width)
        float width = container.resolvedStyle.width;
        if (width <= 0f) width = container.style.width.value.value > 0 ? container.style.width.value.value : 750f;
        // set left to 50% and margin-left negative half width to center
        container.style.left = new StyleLength(new Length(50, LengthUnit.Percent));
        container.style.marginLeft = new StyleLength(new Length(-width * 0.5f, LengthUnit.Pixel));

        // Off-screen starting position (above the top)
        float offTop = -height - 20f;

        // Animate down
        yield return AnimateTop(container, offTop, targetTopPx, slideDuration);

        // Wait while visible
        yield return new WaitForSeconds(displayDuration);

        // Animate up (back off-screen)
        yield return AnimateTop(container, targetTopPx, offTop, slideDuration);

        // Optionally remove the container's label content or keep for reuse.
        // Do not remove container itself as it may be part of UXML.
        container.style.display = DisplayStyle.None;
    }

    private IEnumerator AnimateTop(VisualElement element, float fromPx, float toPx, float duration)
    {
        float elapsed = 0f;
        // set initial
        element.style.top = new StyleLength(new Length(fromPx, LengthUnit.Pixel));

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float value = Mathf.Lerp(fromPx, toPx, t);
            element.style.top = new StyleLength(new Length(value, LengthUnit.Pixel));
            yield return null;
        }

        // ensure final value
        element.style.top = new StyleLength(new Length(toPx, LengthUnit.Pixel));
    }

}