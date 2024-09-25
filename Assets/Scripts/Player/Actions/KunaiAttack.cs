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
    internal class KunaiAttack:MonoBehaviour
    {
        private float kunaiSpawnCooldown = 0.5f;
        private float lastKunaiSpawnTime;


        private Player player;

        //declaring the prefabs
        [SerializeField]
        private GameObject _kunaiPrefab; //prefab


        void Start()
        {
            player = GetComponentInParent<Player>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void SpawnKunai()
        {
            float shootDirection =  player.transform.localScale.x;
            


            Vector3 kunaiSpawningPos = transform.position;
            if (Time.time > kunaiSpawnCooldown + lastKunaiSpawnTime)
            {

                //GameObject KunaiSprite = _kunaiPrefab;
                //if (shootDirection < 0)
                //{
                //    KunaiSprite = _kunaiPrefab.transform.Rotate(0, 0, -180);
                //}
                GameObject kunaiInstance = Instantiate(_kunaiPrefab, kunaiSpawningPos, Quaternion.identity);
                kunaiInstance.GetComponent<Kunai>().SetDirection(shootDirection);
                // Update the last spawn time
                lastKunaiSpawnTime = Time.time;
            }
        }
    }
}
