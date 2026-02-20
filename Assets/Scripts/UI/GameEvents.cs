using System;
using UnityEngine;

public static class GameEvents
{
    // Menu Events
    public static Action HomeScreenShown;
    public static Action SettingsScreenShown;
    public static Action SettingsScreenHidden;
    public static Action PlayGamePressed;
    public static Action OpenMenu;
    public static Action OpenInventory;
    public static Action ExitPressed;

    public static Action<string> CurrentViewChanged;

    // Game Events
    public static Action Movement;
    public static Action Jump;
    public static Action Dash;
    public static Action Sprint;
    public static Action Interact;
}
