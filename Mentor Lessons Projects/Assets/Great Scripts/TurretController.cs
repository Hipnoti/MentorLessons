
using Mentor.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretController : MonoBehaviour
{
    public PlayerController playerController;

    [SerializeField] Transform cannonPivotTransform;

    public void CheckForTarget()
    {
        //if (playerController.HP <= 30)
        //    playerController.HP = 10;
    }

    [ContextMenu("Damage Player")]
    public void DamagePlayer()
    {
    //    playerController.TakeDamage(30, playerController, playerController);
    }

    private void Update()
    {
        Vector3 originalRotationEuler = playerController.transform.eulerAngles;
        cannonPivotTransform.LookAt(playerController.transform);
        cannonPivotTransform.eulerAngles = new Vector3(originalRotationEuler.x,
            cannonPivotTransform.eulerAngles.y, originalRotationEuler.z);
    }
}
