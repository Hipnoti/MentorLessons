using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Mentor.Game
{
    public enum DamageType { Physic, Fire, Ice, Toxic}
    public class PlayerController : MonoBehaviour
    {
       public CharacterStats stats;

        public bool Alive
        {
            get { return hp > 0; }
        }

        [Header("Transform Manipulation")]
        public float movementSpeed = 10f;
        public float rotationSpeed = 30f;

        public Enemy targetEnemy;

        [SerializeField] int hp = 100;

        [Header("Raycast")]
        [SerializeField] Camera mainCamera;
        [SerializeField] LayerMask raycastLayerMask;
        [SerializeField] GameObject explosionHitEffectPrefab;

        [Header("Raycast Debugging")]
        [SerializeField] TextMeshProUGUI mousePositionTextUI;
        [SerializeField] TextMeshProUGUI hitDetectionTextUI;
        [SerializeField] Color32 hitDetectedColor = Color.white;
        [SerializeField] Color32 noHitDetectedColor = Color.white;

        [SerializeField] float messageHandleDebugValue;

   

        //Raycast variables
        Vector2 mousePosition;
        Ray ray;
        bool hitDetected = false;

        #region HPAndStrength

        /// <summary>
        /// Making the character to take damage. When the character reaches HP of 0 and less, he will DIE
        /// </summary>
        /// <param name="damageAmount"> The amount of damage the character takes</param>
        //public void TakeDamage(int damageAmount, params PlayerController[] playerControllers)
        //{
        //    HP -= damageAmount;
        //}

        public void PlayerDied()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
           // gateTrigger.onTriggerHappenedDamage.AddListener(TakeDamage);
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleRaycast();
        }

        private void HandleMovement()
        {
            // Movement forward handling
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
            }
            else
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
            }
        }

        private void HandleRotation()
        {
            //Rotation
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
            }
            else
                if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
            }
        }

        private void HandleRaycast()
        {
            mousePosition = Input.mousePosition;

            mousePositionTextUI.text = mousePosition.ToString();

            RaycastHit raycastHit;
            ray = mainCamera.ScreenPointToRay(mousePosition);
            if (hitDetected = Physics.Raycast(ray, out raycastHit))
            {
                hitDetectionTextUI.text = "Hit Detected";
                hitDetectionTextUI.color = hitDetectedColor;
                if (Input.GetMouseButtonDown(0))
                {
                    //Insert here hit logic
                    GameObject instadEffectObject =
                        Instantiate(explosionHitEffectPrefab, raycastHit.point, explosionHitEffectPrefab.transform.rotation);
                    //  Destroy(instadEffectObject, 3f);
                    //    Destroy(raycastHit.collider.gameObject);

                  //  Debug.Log(targetEnemy.Damage(10, "Logggg"));
                }
            }
            else
            {
                hitDetectionTextUI.text = "No Hit Detected";
                hitDetectionTextUI.color = noHitDetectedColor;
            }
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = hitDetected ? hitDetectedColor : noHitDetectedColor;
                ray = mainCamera.ScreenPointToRay(mousePosition);
                Gizmos.DrawRay(ray.origin, ray.direction * 1000f);

                Gizmos.DrawSphere(transform.position, 1f);
            }
        }

        [ContextMenu("Handle Message Type")]
        string HandleMessage()
        {
            switch (messageHandleDebugValue)
            {
                case 0.5f:
                case 2:
                    return "You are here.";
                default: return "Nothing special here.";
            }
        }
        #endregion

        #region Networking

        #endregion
    }
}
