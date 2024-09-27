using Assets.Scripts.Player.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Assets.Scripts.Game
{
    internal class MySceneManager : MonoBehaviour
    {
        // Static instance to implement Singleton pattern

        private static MySceneManager _instance;
        public static MySceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MySceneManager();
                }
                return _instance;
            }
        }

        [SerializeField] RectTransform fader;

        private void Awake()
        {
            // Ensure that only one instance of MySceneManager exists
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject); // Preserve this object across scene changes
            }
        }

        private void Start()
        {
            // Initialize fader
            fader.gameObject.SetActive(true);

            // SCALE
            LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
            LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                fader.gameObject.SetActive(false);
            });
        }

        public void OpenMenuScene()
        {
            fader.gameObject.SetActive(true);

            // SCALE
            LeanTween.scale(fader, Vector3.zero, 0f);
            LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                SceneManager.LoadScene("Menu");
            });
        }

        public void OpenGameScene(string sceneName)
        {
            fader.gameObject.SetActive(true);

            // SCALE
            LeanTween.scale(fader, Vector3.zero, 0f);
            LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                // Example for little pause before loading the next scene
                StartCoroutine(LoadGameWithDelay(sceneName, 0.5f));
            });
        }

        private void LoadGame(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }


        private IEnumerator LoadGameWithDelay(string sceneName, float delay)
        {
            yield return new WaitForSeconds(delay);
            LoadGame(sceneName);  // Now pass the sceneName parameter correctly
        }
    }
}