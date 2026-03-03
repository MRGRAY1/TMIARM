using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class OverlayHUDUI
{
    public VisualElement root;

    public OverlayHUDUI(VisualElement rootUI)
    {
        root = rootUI;

        this.Initialize();
    }

    public void Initialize()
    {
        root.pickingMode = PickingMode.Ignore;
        root.focusable = false;
        DebugEvents.DebugNotificationMessage += OnNotificationMessage;
    }

    public void Show()
    {
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        root.style.display = DisplayStyle.None;
    }

    private void OnNotificationMessage(object sender, string message)
    {
        ShowNotificationSliding(message);
    }

    // --------------------------------------------------
    // Notification System (kept exactly as requested)
    // --------------------------------------------------

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
        var notificationContainer = root.Q<VisualElement>("NotificationContainer");
        if (notificationContainer == null)
        {
            notificationContainer = new VisualElement { name = "NotificationContainer" };
            notificationContainer.style.position = Position.Absolute;
            notificationContainer.style.left = new StyleLength(new Length(50, LengthUnit.Percent));
            notificationContainer.style.width = 750;
            notificationContainer.style.height = 100;
            notificationContainer.style.alignItems = Align.Center;
            notificationContainer.style.justifyContent = Justify.Center;
            //root.Add(notificationContainer);
        }

        // Ensure there's a label inside the container
        var label = notificationContainer.Q<Label>("NotificationLbl");
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
            notificationContainer.Add(label);
        }

        label.text = text;
        notificationContainer.style.display = DisplayStyle.Flex;

        CoroutineRunner.Coroutines.StartCoroutine(
            ShowNotificationCoroutine(notificationContainer, displayDuration, slideDuration, targetTopPx)
        );
    }

    private IEnumerator ShowNotificationCoroutine(VisualElement container, float displayDuration, float slideDuration, float targetTopPx)
    {
        float height = container.resolvedStyle.height;
        if (height <= 0f) height = 100f;

        float width = container.resolvedStyle.width;
        if (width <= 0f) width = 750f;

        container.style.left = new StyleLength(new Length(50, LengthUnit.Percent));
        container.style.marginLeft = new StyleLength(new Length(-width * 0.5f, LengthUnit.Pixel));

        float offTop = -height - 20f;

        yield return AnimateTop(container, offTop, targetTopPx, slideDuration);
        yield return new WaitForSeconds(displayDuration);
        yield return AnimateTop(container, targetTopPx, offTop, slideDuration);

        container.style.display = DisplayStyle.None;
    }

    private IEnumerator AnimateTop(VisualElement element, float fromPx, float toPx, float duration)
    {
        float elapsed = 0f;
        element.style.top = new StyleLength(new Length(fromPx, LengthUnit.Pixel));

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float value = Mathf.Lerp(fromPx, toPx, t);
            element.style.top = new StyleLength(new Length(value, LengthUnit.Pixel));
            yield return null;
        }

        element.style.top = new StyleLength(new Length(toPx, LengthUnit.Pixel));
    }
}