using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
namespace Assets.Scripts.Weapon
{
    public class Kunai : MonoBehaviour
    {
        [SerializeField]
        private float _kunaiSpeed = 50.0f;
        private float moveDirection;

        public void SetDirection(float direction)
        {
            moveDirection = direction; // Ensure the direction is normalized
        }

        // Start is called before the first frame update
        void Start()
        {
            //transform.Rotate(0, 0, -90);

        }

        // Update is called once per frame
        void Update()
        {
            //translate laser up
            Vector3 kunaiDirection = new Vector3(moveDirection, 0, 0);

            transform.Translate(kunaiDirection* _kunaiSpeed * Time.deltaTime);
            //destroy the laser after some time
            Destroy(this.gameObject, 2.0f); //we can also destroy an object after a certain time


        }
        


    }
}