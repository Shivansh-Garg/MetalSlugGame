using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // all the universal game related configs will go here
    public class AudioManager : MonoBehaviour
    {

       [SerializeField] private AudioSource music;
        [SerializeField] public AudioSource sfx;
        [SerializeField] public AudioClip backgroundMusic;
        [SerializeField] public AudioClip Kunai;
        [SerializeField] public AudioClip fireball;
        [SerializeField] public AudioClip sword;
        [SerializeField] public AudioClip jump;
        [SerializeField] public AudioClip zombies;

        [SerializeField] public AudioClip boss1;
        [SerializeField] public AudioClip boss2;
        [SerializeField] public AudioClip boss3;


        private void Start()
        {
            music.clip = backgroundMusic;

            music.loop = true;

            music.Play();
        }

        public void playSFX(AudioClip clip)
        {

            if (clip != null && !sfx.isPlaying)
            {
                sfx.PlayOneShot(clip);
            }
        }
    }

}