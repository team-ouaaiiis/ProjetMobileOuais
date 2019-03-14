using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : UIButton
{
    [Header("Weapon Features")]
    [SerializeField] private Text weaponName;
    [Range(1, 3)]
    [SerializeField] private int damageValue;
    [Range(1, 3)]
    [SerializeField] private int attackSpeedValue;
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private Sprite tossPath;
    [SerializeField] private Sprite chargingPower;
    [SerializeField] private int price;

    //[SerializeField] private Animator itemTextBox;
}
