using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MapElement
{
    [SerializeField] private string cityName = "";
    [SerializeField] private TMP_Text text;
    [SerializeField] private SpriteRenderer sprite;
    
    public void InitializeCity(string name, Vector2 pos, float scale)
    {
        cityName = name;
        text.text = cityName;
        transform.position = new Vector3(pos.x, 0, pos.y);
        sprite.transform.localScale = new Vector3(scale, scale, scale);
    }

    public string CityName { get => cityName; set => cityName = value; }
}
