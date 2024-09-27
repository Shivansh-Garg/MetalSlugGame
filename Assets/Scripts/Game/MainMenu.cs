using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game
{

    public class MainMenu : MonoBehaviour
    {
        MySceneManager sceneManager; 
        // Method to load the game scene when "Play" is clicked

        public void Start()
        {
            sceneManager = MySceneManager.Instance;
        }
        public void PlayGame()
        {
            // Assuming your game scene is called "GameScene"
            sceneManager.OpenGameScene("Scene_1");
        }

        // Method to open the settings menu (if you have one)
        public void OpenSettings()
        {
            // You can load a settings scene or show/hide a settings UI panel
            Debug.Log("Settings Button Clicked");
        }

        // Method to quit the game when "Quit" is clicked
        public void QuitGame()
        {
            Debug.Log("Quit Button Clicked");
            Application.Quit();  // Works in a built executable. Does nothing in the editor.
        }


        public void OpenMainMenu()
        {
            sceneManager.OpenGameScene("Menu");
        }
    }
}
