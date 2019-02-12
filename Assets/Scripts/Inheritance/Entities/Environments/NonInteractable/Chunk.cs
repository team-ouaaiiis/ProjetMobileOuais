using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : NonInteractable
{
    [SerializeField] private List<Entity> entities = new List<Entity>();
}
