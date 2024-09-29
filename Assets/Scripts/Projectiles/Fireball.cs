using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
namespace Assets.Scripts.Weapon
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField]
        private float _fireballSpeed = 20.0f;
        private float moveDirection;
        private Vector3 localScale;

        public void SetDirection(float direction,Vector3 localScale)
        {
            moveDirection = direction; // Ensure the direction is normalized
            this.localScale = localScale;
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
            Vector3 fireballDirection = new Vector3(moveDirection, 0, 0);

            transform.Translate(fireballDirection * _fireballSpeed * Time.deltaTime);
            transform.localScale = localScale*5;
            //destroy the laser after some time
            Destroy(this.gameObject, 5.0f); //we can also destroy an object after a certain time


        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("ground"))
                Destroy(gameObject);
        }



    }
}