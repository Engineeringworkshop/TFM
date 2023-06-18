using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerControl : MonoBehaviour
{
    public bool isImpacted = false;
    public float displacementSpeed = 1.0f;
    public float rotationSpeedX;
    public float rotationSpeedY;
    public float rotationSpeedZ;

    private void Update()
    {
        if (isImpacted)
        {
            gameObject.transform.Translate(0, 0, Time.deltaTime * displacementSpeed);
            gameObject.transform.Rotate(Time.deltaTime * rotationSpeedX, Time.deltaTime * rotationSpeedY, Time.deltaTime * rotationSpeedZ);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.tag == "Player1")
        {
            var robotController = GetComponent<Robot_Controller>();
            robotController.IsDefending = true;
            robotController.RobotStateMachine.ChangeState(robotController.RobotDefenseState);

            StartCoroutine(DelayImpactMovement());
        }
    }

    private IEnumerator DelayImpactMovement()
    {
        // Wait for the next frame to get the animator info (to wait the next animator state to load)
        yield return new WaitForSeconds(0.1f);

        isImpacted = true;
    }
}
