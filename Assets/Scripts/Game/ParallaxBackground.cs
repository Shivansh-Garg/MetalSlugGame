using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game
{

    public class ParallaxBackground : MonoBehaviour
    {
        public float parallaxEffectMultiplier;  // Multiplier to adjust the parallax speed
        private Transform cameraTransform;      // Reference to the main camera's transform
        private Vector3 lastCameraPosition;     // Stores the camera's position in the previous frame
        private float textureUnitSizeX;         // The width of the background texture
        private bool disabled = true;
        void Start()
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;

            // Get the size of the background texture
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        }

        void Update()
        {
            if (!disabled)
            {

                // Calculate the parallax movement based on the camera's movement
                Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
                transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, 0);

                lastCameraPosition = cameraTransform.position;

                // Loop the background if the camera moves beyond the texture's width
                if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
                {
                    float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                    transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
                }
            }
        }
    }
}