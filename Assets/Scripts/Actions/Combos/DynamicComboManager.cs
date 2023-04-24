using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public enum ComboNodeType
{
    Joystick,
    Buttons,
    Final
}

public class DynamicComboManager : MonoBehaviour
{
    [Header("Node spawn configuration")]
    [SerializeField] private Transform targetReferenceRadius;
    [SerializeField] private ComboNode firstComboNode;
    [SerializeField] private ComboNode currentComboNode;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject targetObj;

    [Header("Gameplay Configuration")]
    [SerializeField] private float distanceDetectionThreshold;

    [Header("Gameplay Configuration")]
    [SerializeField] private GameObject comboJoystickPanel;
    [SerializeField] private GameObject comboButtonPanel;

    [Header("References")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Robot_Controller robot_Controller;   

    [Header("Debug")]
    [SerializeField] private List<GameObject> targetSpawnedList;
    
    public float ReferenceRadius { get; private set; }
    public bool isNodeReached { get; private set; }

    private void Awake()
    {
        comboJoystickPanel.SetActive(false);
        comboButtonPanel.SetActive(false);

        isNodeReached = false;
    }

    // Method to load targets on the screen
    private void LoadTargets()
    {
        if (currentComboNode == null)
        {
            return;
        }

        if (currentComboNode.subComboNodeList.Count <= 0)
        {
            return;
        }

        // Set the initial target position
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        float spawnAngle = Mathf.Deg2Rad * (360 / currentComboNode.subComboNodeList.Count);

        targetSpawnedList.Clear();

        for (int i = 0; i < currentComboNode.subComboNodeList.Count; i++)
        {
            // Calculate target position
            Vector3 currentPosition = new Vector3(transform.position.x + ReferenceRadius * Mathf.Cos(randomAngle + spawnAngle * i), transform.position.y + ReferenceRadius * Mathf.Sin(randomAngle + spawnAngle * i), transform.position.z);

            // Spawn target
            GameObject currentTarget = Instantiate(targetObj, currentPosition, transform.rotation, cursor.transform.parent);

            currentTarget.GetComponent<Image>().sprite = currentComboNode.subComboNodeList[i].targetSprite;

            currentTarget.GetComponent<ComboTarget>().targetComboNode = currentComboNode.subComboNodeList[i];

            // Ad target to the target list
            targetSpawnedList.Add(currentTarget);

            // Debug
            // Debug.Log("Parent " + i + " pos: " + transform.position + " Calculated pos: " + currentPosition);
            // Debug.Log("Target " + i + " pos: " + currentPosition);
        }

        isNodeReached = false;
    }

    private void DestroyTargets()
    {
        foreach (var target in targetSpawnedList)
        {
            Destroy(target);
        }
    }

    public bool CheckDistance(Vector3 joystickPosition, JoysticCombo joystick)
    {
        ComboTarget targetReached = null;

        foreach (var target in targetSpawnedList)
        {
            //print("distance: " + Vector3.Distance(joystickPosition, target.transform.position) + " threshold: " + distanceDetectionThreshold);
            if (Vector3.Distance(joystickPosition, target.transform.position) < distanceDetectionThreshold)
            {
                //Debug.Log("Target reached!");
                targetReached = target.GetComponent<ComboTarget>();

                isNodeReached = true;
            }
        }

        if (isNodeReached && targetReached != null)
        {
            currentComboNode = targetReached.targetComboNode;

            DestroyTargets();

            if (targetReached.targetComboNode.subComboNodeList.Count > 0)
            {
                LoadTargets();

                joystick.ResetPosition();
            }
            else
            {
                OnEndCombo(default);
            }
        }

        return isNodeReached;
    }

    private void LoadNextNode()
    {
        DestroyTargets();

    }

    public void ActiveComboPanel()
    {
        comboJoystickPanel.SetActive(true);

        playerInput.SwitchCurrentActionMap("Combo");

        ReferenceRadius = Vector3.Distance(cursor.transform.position, targetReferenceRadius.position);

        currentComboNode = firstComboNode;

        LoadTargets();
    }

    private void OnRectTransformDimensionsChange()
    {
        // TODO method to update parameters when the resolution changes
    }

    #region Controls

    public void OnEndCombo(InputAction.CallbackContext value)
    {
        // Destroy all targets
        DestroyTargets();

        // Deactivate panels
        comboJoystickPanel.SetActive(false);
        comboButtonPanel.SetActive(false);

        robot_Controller.SetNextAttack(currentComboNode.attackData);

        // Switch action map
        playerInput.SwitchCurrentActionMap("Action");
        
    }

    #endregion
}
