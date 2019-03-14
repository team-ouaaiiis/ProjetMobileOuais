using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager
{
    public static UIManager instance;
    private List<Entity> entities = new List<Entity>();

    public override void Awake()
    {
        base.Awake();
        Initialize();
    }

    [ContextMenu("Initialize")]
    private void Initialize()
    {
        entities = GameManager.instance.Entities;
        instance = this;
    }

    public void CallSwapCharacter()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].SwapCharacter();
        }
    }

    public void CallShopBubbleSelection(int iD)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].ShopBubbleSelection(iD);
        }
    }

    public void CallShopBubbleSpawn(bool isOn)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            if (isOn)
                entities[i].ShopBubbleSpawn();
            else
                entities[i].ShopBubbleDespawn();
        }
    }

    public void CallShopRackMove(bool isRight)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].ShopRackMove(isRight);
        }
    }

    public List<Entity> Entities { get => entities; }
}
