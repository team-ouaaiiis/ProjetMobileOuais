using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MapElement
{
    [SerializeField] private string cityName = "";
    [SerializeField] private TMP_Text text;
    [SerializeField] private SpriteRenderer sprite;
    
    public void InitializeCity(string name, Vector2 pos, float scale, float nameSize)
    {
        cityName = name;
        text.text = cityName;
        transform.position = new Vector3(pos.x, 0, pos.y);
        sprite.transform.localScale = new Vector3(scale, scale, scale);
        text.fontSize = scale * nameSize;
        text.transform.localPosition = new Vector3(0, Mathf.Sign(Random.Range(-1, 1)) * scale / 3f, 0);
    }

    public string CityName { get => cityName; set => cityName = value; }
}
