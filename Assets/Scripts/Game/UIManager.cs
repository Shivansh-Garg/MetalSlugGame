using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    internal class UIManager:MonoBehaviour
    {

        
        public TMP_Text ScoreText;
        public TMP_Text FireballText;
        public TMP_Text KunaiText;


        public RectTransform fader;
        private static UIManager _instance;

        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UIManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("UIManager instance not found in the scene. Please ensure UIManager is present in the scene.");
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void UpdateScoreUI(int score)
        {
            ScoreText.text = score.ToString();
        }


        public void UpdateFireballs(int fireballs)
        {
            FireballText.text = fireballs.ToString();
        }


    }
}
