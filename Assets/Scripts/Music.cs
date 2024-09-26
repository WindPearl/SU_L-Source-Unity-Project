using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public AudioSource music;
    public AudioSource superSonicMusic;
    public AudioMixerGroup audioMixerGroup;

    //public AudioClip music;
    //public AudioClip superSonicMusic;
   
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        void StateChangeToSuperSonicStart()
        {
            superSonicMusic = GetComponent<AudioSource>();
            superSonicMusic.Play();
            music.Stop();
        }
    }
}
