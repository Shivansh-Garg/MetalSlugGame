using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Assets.Scripts.Weapon;

namespace Assets.Scripts.Player.Actions
{
    internal class FireballAttack : MonoBehaviour
    {
        private float fireballSpawnCooldown = 1f;
        private float lastFireballSpawnTime;
        private float fireballQuantity;


        private Player player;

        //declaring the prefabs
        [SerializeField]
        private GameObject _fireballPrefab; //prefab


        void Start()
        {
            player = GetComponentInParent<Player>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void increaseFireballCount()
        {
            fireballQuantity++;
        }

        public void SpawnFireball()
        {
            float shootDirection = player.transform.localScale.x;
            Vector3 fireballLocalScale = new Vector3(1, 1, 1);
            if (player.transform.localScale.x < 0.0f)
            {
                fireballLocalScale = new Vector3(-1, 1, 1);
            }



            Vector3 fireballSpawningPos = transform.position;
            if (Time.time > fireballSpawnCooldown + lastFireballSpawnTime)
            {

                //GameObject KunaiSprite = _kunaiPrefab;
                //if (shootDirection < 0)
                //{
                //    KunaiSprite = _kunaiPrefab.transform.Rotate(0, 0, -180);
                //}
                GameObject fireballInstance = Instantiate(_fireballPrefab, fireballSpawningPos, Quaternion.identity);
                fireballInstance.GetComponent<Fireball>().SetDirection(shootDirection,fireballLocalScale);
                // Update the last spawn time
                lastFireballSpawnTime = Time.time;
            }
        }
    }
}
