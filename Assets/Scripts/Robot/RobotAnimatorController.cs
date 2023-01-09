using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimatorController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Robot_Controller robotController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void FreezeAnimation()
    {
        if (Input.GetKey(robotController.robotData.defenseKey))
        {
            Debug.Log("Frezze animation");

            animator.speed = 0f;
        }
    }

    public void UnFreezeAnimation()
    {
        animator.speed = 1f;
    }

    public void AttackTrigger()
    {
        robotController.AttackTrigger();
    }
}
