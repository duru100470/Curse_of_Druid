using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItem : Item
{
    public override bool OnUse(Entity user)
    {
        Debug.Log("Item is used");
        Durability--;
        return true;
    }
}