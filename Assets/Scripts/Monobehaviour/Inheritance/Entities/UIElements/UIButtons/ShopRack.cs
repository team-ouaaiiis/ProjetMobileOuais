using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRack : Entity
{
    [SerializeField] private Transform[] weaponList;
    [SerializeField] private Transform[] medalList;
    [SerializeField] private Transform[] armorList;
    [SerializeField] private Transform[] helmetList;

    private int lastID;
    private int lastLength;
    private int rackMoveStep;
    private Vector3 targetPos;
    private Vector3 originPos;

    public override void Start()
    {
        base.Start();

        for (int i = 0; i < helmetList.Length; i++)
        {
            helmetList[i].position = new Vector3(i * 64 + 79, -35, 0);
        }
        for (int i = 0; i < medalList.Length; i++)
        {
            medalList[i].position = new Vector3(i * 64 + 79, -35, 0);
        }
        for (int i = 0; i < armorList.Length; i++)
        {
            armorList[i].position = new Vector3(i * 64 + 79, -35, 0);
        }
        for (int i = 0; i < weaponList.Length; i++)
        {
            weaponList[i].position = new Vector3(i * 64 + 79, -35, 0);
        }

        targetPos = Vector3.zero;
        originPos = Vector3.zero;
    }

    public override void Update()
    {
        base.Update();

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * 10);
    }

    public override void ShopBubbleSelection(int ID)
    {
        ListDesactivator();
        lastID = ID;
        rackMoveStep = 0;
        transform.localPosition = Vector3.zero;
        targetPos = Vector3.zero;

        if (ID == 1)
        {
            for (int i = 0; i < helmetList.Length; i++)
            {
                helmetList[i].gameObject.SetActive(true);
            }
            lastLength = helmetList.Length;
        }
        if (ID == 2)
        {
            for (int i = 0; i < armorList.Length; i++)
            {
                armorList[i].gameObject.SetActive(true);
            }
            lastLength = armorList.Length;
        }
        if (ID == 3)
        {
            for (int i = 0; i < weaponList.Length; i++)
            {
                weaponList[i].gameObject.SetActive(true);
            }
            lastLength = weaponList.Length;
        }
        if (ID == 4)
        {
            for (int i = 0; i < medalList.Length; i++)
            {
                medalList[i].gameObject.SetActive(true);
            }
            lastLength = medalList.Length;
        }
    }

    private void ListDesactivator()
    {
        if (lastID == 1)
        {
            for (int i = 0; i < helmetList.Length; i++)
            {
                helmetList[i].gameObject.SetActive(false);
            }
        }
        if (lastID == 2)
        {
            for (int i = 0; i < armorList.Length; i++)
            {
                armorList[i].gameObject.SetActive(false);
            }
        }
        if (lastID == 3)
        {
            for (int i = 0; i < weaponList.Length; i++)
            {
                weaponList[i].gameObject.SetActive(false);
            }
        }
        if (lastID == 4)
        {
            for (int i = 0; i < medalList.Length; i++)
            {
                medalList[i].gameObject.SetActive(false);
            }
        }
    }

    public override void ShopRackMove(bool isRight)
    {
        if (isRight)
        {
            if (rackMoveStep - 3 > -lastLength)
            {
                rackMoveStep--;
            }
        }

        if (!isRight)
        {
            if (rackMoveStep < 0)
            {
                rackMoveStep++;
            }
        }

        targetPos = new Vector3(originPos.x + rackMoveStep * 64, 0, 0);
    }
}
