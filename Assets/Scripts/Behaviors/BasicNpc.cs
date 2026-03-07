using UnityEngine;

public class BasicNpc : NpcInteractions
{
    public override void Interact()
    {
        base.Interact();
        Logger.Log($"Npc {NPCName} hit");
    }
}