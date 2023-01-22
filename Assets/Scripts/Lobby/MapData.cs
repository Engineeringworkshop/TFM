using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMapData", menuName = "Data/Map Data")]
public class MapData : ScriptableObject
{
    [Header("Map properties")]
    public Sprite mapPreviewImage;
    public string mapName;
}
