using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.InputSystem;


public class OrcAgentController : MonoBehaviour
{
    public Transform[] waypoints;
    public NavMeshAgent navMeshAgent;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator orcAnimator;
    [SerializeField] private bool goOnStart = false;
    [SerializeField] private bool canLoop = false;
    [SerializeField] float remainingDistanceTrigger;

    private int currentWaypointIndex = 0;

    #region InputEvents

    public void OnLaunch(InputAction.CallbackContext callbackContext)
    {
        Debug.Log(callbackContext.phase);
        
        if(callbackContext.phase == InputActionPhase.Performed)
            StartMovement();
    }

    

    #endregion

    // private Keyboard currentKeyboard;
    // private Mouse currentMouse;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Orc Start");
        if (goOnStart)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
        
        // currentKeyboard = Keyboard.current;
        // currentMouse = Mouse.current;
    }

    private void Update()
    {
        // if(currentMouse.leftButton.wasPressedThisFrame || currentKeyboard.spaceKey.wasPressedThisFrame)
        //     StartMovement();
        if (!navMeshAgent.isStopped)
        {
            orcAnimator.SetFloat("Velocity", navMeshAgent.velocity.magnitude);
            if (navMeshAgent.remainingDistance <= remainingDistanceTrigger)
            {
                currentWaypointIndex++;


                if (currentWaypointIndex >= waypoints.Length)
                {
                    if (canLoop)
                        currentWaypointIndex = 0;
                }

                if (currentWaypointIndex < waypoints.Length)
                    navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
            }

        }
    }

    [ContextMenu("Start Move")]
    void StartMovement()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }

   
}
