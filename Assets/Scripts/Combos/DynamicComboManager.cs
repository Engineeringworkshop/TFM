using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicComboManager : MonoBehaviour
{
    [Header("Node spawn configuration")]
    [SerializeField] private Transform targetReferenceRadius;
    [SerializeField] private List<Target> currentTargetList;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject targetObj;

    [Header("Debug")]
    [SerializeField] private List<GameObject> targetSpawnedList;
    [SerializeField] private float referenceRadius;

    private void Start()
    {
        referenceRadius = Vector3.Distance(cursor.transform.position, targetReferenceRadius.position);

        LoadTargets();
    }

    // Method to load targets on the screen
    private void LoadTargets()
    {
        // Set the initial target position
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        float spawnAngle = Mathf.Deg2Rad * (360 / currentTargetList.Count);

        for (int i = 0; i < currentTargetList.Count; i++)
        {
            // Calculate target position
            Vector3 currentPosition = new Vector3(transform.position.x + referenceRadius * Mathf.Cos(randomAngle + spawnAngle * i), transform.position.y + referenceRadius * Mathf.Sin(randomAngle + spawnAngle * i), transform.position.z);

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Target")
        {
            Debug.Log("Target reached");

            DestroyTargets();
        }
    }

    private void OnRectTransformDimensionsChange()
    {
        //Do the things
    }
}
