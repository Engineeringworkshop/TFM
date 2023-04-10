using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicComboManager : MonoBehaviour
{
    [SerializeField] private Transform targetReferenceRadius;
    [SerializeField] private List<Target> currentTargetList;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject target;

    [SerializeField] private float referenceRadius;

    private void Start()
    {
        LoadTargets();

        referenceRadius = Vector3.Distance(transform.position, targetReferenceRadius.position);
    }

    // Method to load targets on the screen
    private void LoadTargets()
    {
        // Set the initial target position
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        Vector3 currentPosition = new Vector3(transform.position.x*Mathf.Cos(randomAngle), transform.position.y * Mathf.Sin(randomAngle), transform.position.z);

        foreach (var target in currentTargetList)
        {
            // Spawn targets
            var currentTarget = Instantiate(target, currentPosition, transform.rotation, transform.parent);
            currentTargetList.Add(currentTarget);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Target")
        {
            Debug.Log("Target reached");
        }
    }

    private void OnRectTransformDimensionsChange()
    {
        //Do the things
    }
}
