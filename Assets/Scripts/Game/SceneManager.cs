using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Assets.Scripts.Game
{
    public class MySceneManager : MonoBehaviour
    {
        //[SerializeField] private RectTransform fader;

        // Singleton instance
        private static MySceneManager _instance;

        public static MySceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MySceneManager>();
                }
                return _instance;
            }
        }

        private void Start()
        {
            //fader.gameObject.SetActive(true);

            // ALPHA
            // LeanTween.alpha (fader, 1, 0);
            // LeanTween.alpha (fader, 0, 0.5f).setOnComplete (() => {
            //     fader.gameObject.SetActive (false);
            // });

            // SCALE
            //LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
            //LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            //    fader.gameObject.SetActive(false);
            //});
        }



        public void OpenMenuScene()
        {
            //fader.gameObject.SetActive(true);
            //LeanTween.scale(fader, Vector3.zero, 0f);
            //LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            //{
                SceneManager.LoadScene("Menu");
            //});
        }

        public void OpenGameScene(string sceneName)
        {
            //fader.gameObject.SetActive(true);
            //LeanTween.scale(fader, Vector3.zero, 0f);
            //LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            //{
                StartCoroutine(LoadGameWithDelay(sceneName, 0.5f));
            //});
        }

        public void GetCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator LoadGameWithDelay(string sceneName, float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(sceneName);
        }
    }
}