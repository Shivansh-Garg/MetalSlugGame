using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game { 

    public class InteractionSystem : MonoBehaviour
    {
        //detection point
        public Transform detectionPoint;
        private const float detectionRadius = 0.2f;
        public LayerMask detectionLayer;


        // Start is called before the first frame update
        void Start()
        {   
        
        }

        // Update is called once per frame
        void Update()
        {
            if (DetectObject())
            {
                Debug.Log
                if (InteractInput())
                {
                    Debug.Log("Detected");
                }
            }
        }


        bool InteractInput()
        {
            return Input.GetKeyDown(KeyCode.E);
        }

        bool DetectObject()
        {
            bool detected = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
            return detected;
        }
    }

}