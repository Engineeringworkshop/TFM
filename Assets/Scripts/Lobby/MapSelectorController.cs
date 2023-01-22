using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MapSelectorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<MapData> mapDataList;
    [SerializeField] private Image mapSelectedImage;
    [SerializeField] private TMP_Text mapNameText;

    [Header("Debug")]
    [SerializeField] private int currentMapIndex;

    private void OnValidate()
    {
        if (mapDataList != null && mapDataList.Count > 0 && mapSelectedImage != null)
        {
            mapSelectedImage.sprite = mapDataList[0].mapPreviewImage;
        }    
    }

    private void Awake()
    {
        currentMapIndex = 0;

        UpdateMapSelectedImage();
    }

    private void UpdateMapSelectedImage()
    {
        if (mapDataList != default && mapDataList.Count > 0)
        {
            mapSelectedImage.sprite = mapDataList[currentMapIndex].mapPreviewImage;

            mapNameText.text = mapDataList[currentMapIndex].mapName;
        }
        else
        {
            Debug.Log("No maps added to mapa list");
        }
    }

    /// <summary>
    /// Method to load current selected scene
    /// </summary>
    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(currentMapIndex + 2);
    }

    public void NextMap()
    {
        if (currentMapIndex + 1 == mapDataList.Count)
        {
            currentMapIndex = 0;
        }
        else
        {
            currentMapIndex++;
        }

        UpdateMapSelectedImage();
    }

    public void PrevMap()
    {
        if (currentMapIndex == 0)
        {
            currentMapIndex = mapDataList.Count - 1;
        }
        else
        {
            currentMapIndex--;
        }

        UpdateMapSelectedImage();
    }
}
