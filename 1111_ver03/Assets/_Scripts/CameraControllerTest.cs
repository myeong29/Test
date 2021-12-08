using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Photon.Pun.Urachacha
{
    public class CameraControllerTest : MonoBehaviourPun
    {
        [SerializeField] private float mouseSensitivity;

        private Transform parent;

        private void Start() 
        {
            parent = transform.parent;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void Update() 
        {
            Rotate();
        }

        private void Rotate()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            parent.Rotate(Vector3.up, mouseX);
        }
    }
}