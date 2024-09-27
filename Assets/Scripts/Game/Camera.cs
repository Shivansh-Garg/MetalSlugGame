using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{


    
    public class Camera : MonoBehaviour
    {
        private Camera cam;

        public float zoomSpeed = 2.0f;  // Speed of zooming in/out
        public float minZoom = 5.0f;    // Minimum zoom level
        public float maxZoom = 20.0f;   // Maximum zoom level

        // Camera boundary values (change these according to your game world)
        public float minX = 3f;  // Minimum X clamp value
        public float maxX = 220f;   // Maximum X clamp value
        public float minY = -7f;   // Minimum Y clamp value
        public float maxY = 6.77f;    // Maximum Y clamp value

        public Vector3 offset;    // The offset distance between the player and camera
        public float smoothSpeed = 0.225f;  // Smoothing factor to make camera movement smooth
        public GameObject player; // Reference to the player's transform


        void LateUpdate()
        {
            // Ensure player reference is assigned only once instead of every frame
            if (player == null)
            {
                player = GameObject.Find("Player");
            }

            float offsetXPos = 5.0f;
            float offsetYPos = 2.0f;

            

            // Flip the offset based on player's local scale
            if (player.transform.localScale.x < 0)
            {
                offsetXPos = -Mathf.Abs(offsetXPos);
            }

            float playerXPos = player.transform.position.x;
            float playerYPos = player.transform.position.y;

            if (playerXPos < 7.0f)
            {
                playerXPos = 7.0f;
            }

            if(playerYPos<-5.0f)
            {
                playerYPos = -5.0f;
            }
            else if (playerYPos > 0f)
            {
                playerYPos = 0;
            }
            
            
            // Target position for the camera (player's position + adjusted offset)
            Vector3 desiredPosition = new Vector3(playerXPos + offsetXPos, playerYPos + offsetYPos, transform.position.z);
            float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // Smoothly interpolate the camera's position towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime * 1f);


            transform.position = smoothedPosition;
            //transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
        }


        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();
            transform.position = new Vector3(17.3f, 0.76f, -10f);

 
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}