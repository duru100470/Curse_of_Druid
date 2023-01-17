using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleTile : Tile
{
    [Header("RuleTile")]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    protected List<TileRule> tileRuleList;
    private List<Func<bool>> funcList;

    public List<TileRule> TileRuleList => tileRuleList;


    protected virtual void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        funcList = new List<Func<bool>>();

        // Add func list
        funcList.Add(IsThereLeftUpTile);
        funcList.Add(IsThereUpTile);
        funcList.Add(IsThereRightUpTile);
        funcList.Add(IsThereLeftTile);
        funcList.Add(null);
        funcList.Add(IsThereRightTile);
        funcList.Add(IsThereLeftDownTile);
        funcList.Add(IsThereDownTile);
        funcList.Add(IsThereRightDownTile);
    }
    
    public virtual void UpdateRuleTile()
    {
        foreach (var rule in tileRuleList)
        {
            var ret = true;

            for (int i = 0; i < 9; i++)
            {
                if (i == 4) continue;

                switch (rule.tileRuleInfo[i])
                {
                    case TileRule.TILERULE_STATE.This:
                        ret &= funcList[i]();
                        break;
                    case TileRule.TILERULE_STATE.NotThis:
                        ret &= !funcList[i]();
                        break;
                    case TileRule.TILERULE_STATE.None:
                        break;
                }
            }

            if (ret)
                spriteRenderer.sprite = rule.sprite;
        }
    }

    private bool IsThereLeftTile()
    {
        Tile target;
        if (!TileManager.Inst.TileDict.TryGetValue(Pos + new Coordinate(-1, 0), out target))
            return false;
        if (target.TileId == this.TileId)
            return true;
        else
            return false;
    }

    private bool IsThereRightTile()
    {
        Tile target;
        if (!TileManager.Inst.TileDict.TryGetValue(Pos + new Coordinate(1, 0), out target))
            return false;
        if (target.TileId == this.TileId)
            return true;
        else
            return false;
    }

    private bool IsThereUpTile()
    {
        Tile target;
        if (!TileManager.Inst.TileDict.TryGetValue(Pos + new Coordinate(0, 1), out target))
            return false;
        if (target.TileId== this.TileId)
            return true;
        else
            return false;
    }

    private bool IsThereDownTile()
    {
        Tile target;
        if (!TileManager.Inst.TileDict.TryGetValue(Pos + new Coordinate(0, -1), out target))
            return false;
        if (target.TileId== this.TileId)
            return true;
        else
            return false;
    }

    private bool IsThereLeftUpTile()
    {
        Tile target;
        if (!TileManager.Inst.TileDict.TryGetValue(Pos + new Coordinate(-1, 1), out target))
            return false;
        if (target.TileId== this.TileId)
            return true;
        else
            return false;
    }

    private bool IsThereRightUpTile()
    {
        Tile target;
        if (!TileManager.Inst.TileDict.TryGetValue(Pos + new Coordinate(1, 1), out target))
            return false;
        if (target.TileId== this.TileId)
            return true;
        else
            return false;
    }

    private bool IsThereLeftDownTile()
    {
        Tile target;
        if (!TileManager.Inst.TileDict.TryGetValue(Pos + new Coordinate(-1, -1), out target))
            return false;
        if (target.TileId== this.TileId)
            return true;
        else
            return false;
    }

    private bool IsThereRightDownTile()
    {
        Tile target;
        if (!TileManager.Inst.TileDict.TryGetValue(Pos + new Coordinate(1, -1), out target))
            return false;
        if (target.TileId== this.TileId)
            return true;
        else
            return false;
    }
}

[Serializable]
public struct TileRule
{
    public Sprite sprite;
    [SerializeField]
    public TILERULE_STATE[] tileRuleInfo;

    public TileRule(Sprite sprite, TILERULE_STATE[] tileRuleInfo)
    {
        this.sprite = sprite;
        this.tileRuleInfo = tileRuleInfo;
    }

    [Serializable]
    public enum TILERULE_STATE
    {
        None,
        This,
        NotThis
    }
}