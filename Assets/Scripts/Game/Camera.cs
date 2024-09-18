using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    
    public class Camera : MonoBehaviour
    {

        public Transform player;  // Reference to the player's transform
        public Vector3 offset;    // The offset distance between the player and camera
        public float smoothSpeed = 0.125f;  // Smoothing factor to make camera movement smooth


        void LateUpdate()
        {
            // Target position the camera should move towards (player's position + offset)
            Vector3 desiredPosition = player.position + offset;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}