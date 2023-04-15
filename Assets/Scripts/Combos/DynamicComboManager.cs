using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DynamicComboManager : MonoBehaviour
{
    [Header("Node spawn configuration")]
    [SerializeField] private Transform targetReferenceRadius;
    [SerializeField] private List<Target> currentTargetList;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject targetObj;

    [SerializeField] private float distanceDetectionThreshold = 1.0f;

    [Header("Debug")]
    [SerializeField] private List<GameObject> targetSpawnedList;
    public float ReferenceRadius { get; private set; }

    private void Start()
    {
        ReferenceRadius = Vector3.Distance(cursor.transform.position, targetReferenceRadius.position);

        LoadTargets();
    }

    // Method to load targets on the screen
    private void LoadTargets()
    {
        // Set the initial target position
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        float spawnAngle = Mathf.Deg2Rad * (360 / currentTargetList.Count);

        targetSpawnedList.Clear();

        for (int i = 0; i < currentTargetList.Count; i++)
        {
            // Calculate target position
            Vector3 currentPosition = new Vector3(transform.position.x + ReferenceRadius * Mathf.Cos(randomAngle + spawnAngle * i), transform.position.y + ReferenceRadius * Mathf.Sin(randomAngle + spawnAngle * i), transform.position.z);

            // Spawn target
            GameObject currentTarget = Instantiate(targetObj, currentPosition, transform.rotation, cursor.transform.parent);

            // Ad target to the target list
            targetSpawnedList.Add(currentTarget);

            // Debug
            // Debug.Log("Parent " + i + " pos: " + transform.position + " Calculated pos: " + currentPosition);
            // Debug.Log("Target " + i + " pos: " + currentPosition);
        }
    }

    private void DestroyTargets()
    {
        foreach (var target in targetSpawnedList)
        {
            Destroy(target);
        }
    }

    public bool CheckDistance(Vector3 joystickPosition)
    {
        foreach (var target in targetSpawnedList)
        {
            //print("distance: " + Vector3.Distance(joystickPosition, target.transform.position) + " threshold: " + distanceDetectionThreshold);
            if (Vector3.Distance(joystickPosition, target.transform.position) < distanceDetectionThreshold)
            {
                //Debug.Log("Target reached!");

                // TODO lógica al alcanzar el nodo
                DestroyTargets();
                LoadTargets();
                return true;
            }
        }

        return false;
    }

    private void OnRectTransformDimensionsChange()
    {
        //Do the things
    }
}
