using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityReferences : MonoBehaviour
{
    [SerializeField] private TMP_Text textChunk;

    public TMP_Text TextChunk { get => textChunk; set => textChunk = value; }
}
