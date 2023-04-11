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

        bool isFirstTarget = false;

        Vector3 currentPosition;

        Vector3 previousPosition = transform.position;

        foreach (var target in currentTargetList)
        {
            if (!isFirstTarget)
            {
                // Spawn target
                GameObject currentTarget = Instantiate(targetObj, transform.position, transform.rotation, cursor.transform.parent);

                // Move target to the final position
                currentPosition = new Vector3(transform.position.x + referenceRadius * Mathf.Cos(randomAngle), transform.position.y + referenceRadius * Mathf.Sin(randomAngle), transform.position.z);

                Debug.Log("Parent 1 pos: " + transform.position + " Calculated pos: " + currentPosition);

                currentTarget.transform.position = currentPosition;

                previousPosition = currentPosition;

                Debug.Log("Target 1 pos: " + currentPosition);

                isFirstTarget = true;

                targetSpawnedList.Add(currentTarget);
            }
            else
            {
                // Spawn target
                GameObject currentTarget = Instantiate(targetObj, previousPosition, transform.rotation, cursor.transform.parent);

                // Rotate target
                currentTarget.transform.Rotate(cursor.transform.forward, 360/currentTargetList.Count);

                currentPosition = currentTarget.transform.position;

                targetSpawnedList.Add(currentTarget);

                Debug.Log("Target 2 pos: " + currentPosition);
            }
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
