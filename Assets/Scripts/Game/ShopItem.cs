using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    public enum ItemType
    {
        Jump,
        WallGrab,
        WallJump,
        Dash
    }

    public static int GetCost(ItemType type)
    {
        switch (type)
        {
            default:                    return 0;
            case ItemType.Jump:         return 10;
            case ItemType.WallGrab:     return 15;
        }
    }
}
