using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;

    [SerializeField] private float framingFactor;
    [SerializeField] private float minDistance;
    [SerializeField] private float yOffset;

    [SerializeField] private Camera mainCamera;

    private bool isShaking;

    private void OnValidate()
    {
        if (gameplayManager == null)
        {
            gameplayManager = FindObjectOfType<GameplayManager>();
        }

        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }

    }

    private void Awake()
    {
        isShaking = false;
    }

    private void Start()
    {
        SetCameraPosition();
    }

    private void Update()
    {
        SetCameraPosition();
    }

    private void SetCameraPosition()
    {
        Vector3 center = CalculateMiddlePosition();

        // Set camera.Z position to middle position
        transform.position = new Vector3(CalculateCameraDistance(center.x), center.y + yOffset, center.z);
        transform.forward = new Vector3(center.x, center.y + yOffset, center.z) - transform.position;
    }

    private Vector3 CalculateMiddlePosition()
    {
        float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;

        foreach (var player in gameplayManager.PlayerList)
        {
            totalX += player.transform.position.x;
            totalY += player.transform.position.y;
            totalZ += player.transform.position.z;
        }

        return new Vector3 (
            totalX / gameplayManager.PlayerList.Count,
            totalY / gameplayManager.PlayerList.Count,
            totalZ / gameplayManager.PlayerList.Count);
    }

    private float CalculateMaxDistanceBetweenTargets()
    {
        float maxDistance = 0.0f;

        // Look for the maximun distance between players
        for (int i = 0; i < gameplayManager.PlayerList.Count; i++)
        {
            for (int j = i + 1; j < gameplayManager.PlayerList.Count; j++)
            {
                float currDistance = Mathf.Abs(
                    Vector3.Distance(
                        gameplayManager.PlayerList[i].transform.position,
                        gameplayManager.PlayerList[j].transform.position));

                if (currDistance > maxDistance)
                {
                    maxDistance = currDistance;
                }
            }
        }

        return maxDistance;
    }

    private float CalculateCameraDistance(float xOffset)
    {
        float cameraDistance = ((CalculateMaxDistanceBetweenTargets() / 2) * (1 + framingFactor))
            / Mathf.Tan(Mathf.Deg2Rad * mainCamera.fieldOfView/2);

        return Mathf.Max(cameraDistance + xOffset, minDistance);
    }

    public void StartCameraShake()
    {
        StartCoroutine(CameraShake(Random.Range(0.1f, 0.5f)));
    }

    private IEnumerator CameraShake(float time)
    {
        float randomShake = Random.Range(0.1f, 0.5f);
        float timeCounter = 0f;

        do
        {
            mainCamera.transform.localPosition = mainCamera.transform.localPosition + Random.insideUnitSphere * randomShake;

            timeCounter += Time.deltaTime;

            yield return null;

        } while (timeCounter < time);
    }
}
