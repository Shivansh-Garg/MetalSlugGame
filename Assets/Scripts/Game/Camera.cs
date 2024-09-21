using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    
    public class Camera : MonoBehaviour
    {

        public Vector3 offset;    // The offset distance between the player and camera
        public float smoothSpeed = 0.225f;  // Smoothing factor to make camera movement smooth
        public GameObject player; // Reference to the player's transform


        void LateUpdate()
        {
            player = GameObject.Find("Player");

            float playerXPos = player.transform.position.x;
            float offsetXPos = 5.0f;

            // Target position the camera should move towards (player's position + offset)
            Vector3 desiredPosition = new Vector3(playerXPos + offsetXPos, transform.position.y, transform.position.z); 

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;
        }


        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector3(5.0f, 0, -10);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}