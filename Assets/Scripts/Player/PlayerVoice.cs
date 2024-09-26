using UnityEngine;
using UnityEngine.Audio;

public class PlayerVoice : MonoBehaviour
{
    public VoiceType voiceType;
    public AudioMixerGroup audioMixerGroup;

    public AudioClip[] generationsJump;
    public AudioClip[] unleashedJump;

    public AudioClip[] generationsBoost;
    public AudioClip[] unleashedBoost;

    public AudioClip[] generationsStumble;
    public AudioClip[] unleashedStumble;

    public AudioClip[] generationsQuickstep;
    public AudioClip[] unleashedQuickstep;

    private AudioSource audioSource;

    public AudioClip trick;
    public AudioClip trickFail;
    public AudioClip allRight;
    public AudioClip Drift;
    public AudioClip Push;

    private float driftAudioCharge;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.dopplerLevel = 0;
        audioSource.maxDistance = 100;
        audioSource.minDistance = 2;
        audioSource.volume = 0.5f;
        audioSource.priority = 256;
    }
    
    private void PlayRandomSound(AudioClip[] audioClips)
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        // Old version
        //         audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)]);
    }

    private void StateBoostStart()
    {
        if (!audioSource.isPlaying)
        {
        switch (voiceType)
        {
            case VoiceType.Generations:
                PlayRandomSound(generationsBoost);
                break;
            case VoiceType.Unleashed:
                PlayRandomSound(unleashedBoost);
                break;
        }
        }

    }
    private void StateAirboostStart()
    {
        if (!audioSource.isPlaying)
        {
        switch (voiceType)
        {
            case VoiceType.Generations:
                PlayRandomSound(generationsBoost);
                break;
            case VoiceType.Unleashed:
                PlayRandomSound(unleashedBoost);
                break;
        }
        }

    }

    void StateJumpCollisionStart()
    {
        if (Player.instance.isSnowboarding)
        {
            //audioSource.PlayOneShot(trick);
            PlayRandomSound(unleashedJump);
        }
    }

    private void StateJumpStart()
    {
        if (!audioSource.isPlaying)
        {
         switch (voiceType)
         {
             case VoiceType.Generations:
                 PlayRandomSound(generationsJump);
                 break;
             case VoiceType.Unleashed:
                 PlayRandomSound(unleashedJump);
                 break;
         }
        }

    }

    private void StateGrindJumpStart()
    {
        StateJumpStart();
    }

    private void StateGrindSwitchLeftStart()
    {
        StateJumpStart();
    }

    private void StateGrindSwitchRightStart()
    {
        StateJumpStart();
    }

    private void StateHurdleStart()
    {
        StateJumpStart();
    }

    void StatePulleyStart()
    {
        audioSource.PlayOneShot(allRight);
    }

    void TrickJumpSuccess()
    {
        audioSource.PlayOneShot(trick);
    }

    void TrickJumpFail()
    {
        audioSource.PlayOneShot(trickFail);
    }

    private void StateStumbleStart()
    {
        PlayRandomSound(unleashedStumble);
        /*
        switch (voiceType)
        {
            case VoiceType.Generations:
                PlayRandomSound(generationsStumble);
                break;
            case VoiceType.Unleashed:
                PlayRandomSound(unleashedStumble);
                break;
        }
        */
    }
    private void StateQuickstepLeftStart()
    {
        if (!audioSource.isPlaying)
        {
         switch (voiceType)
         {
             case VoiceType.Generations:
                 PlayRandomSound(generationsQuickstep);
                 break;
             case VoiceType.Unleashed:
                 PlayRandomSound(unleashedQuickstep);
                 break;
         }
        }

    }
    private void StateQuickstepRightStart()
    {
        if (!audioSource.isPlaying)
        {
        switch (voiceType)
        {
            case VoiceType.Generations:
                PlayRandomSound(generationsQuickstep);
                break;
            case VoiceType.Unleashed:
                PlayRandomSound(unleashedQuickstep);
                break;
        }
        }

    }
    private void StateDriftStart()
    {
        driftAudioCharge = 1;
        /*
       driftAudioDelayIniciator = +1;
       driftAudioDelay += Time.deltaTime;
        if (audioSource.name == ("Drift & Push") && audioSource.isPlaying == false)
        {
          audioSource.PlayOneShot(Drift);
        }
     
        if (driftAudioDelayIniciator == 1)
        {
            driftAudioDelay += Time.deltaTime;
        }
    
        if (audioSource.isPlaying.Drift == false)
        {
            audioSource.PlayOneShot(Drift);
        }
        */
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Drift);
        }
    }
    private void StateDrift()
    {
        Debug.Log("State Drift (Audio)");
    }

    private void StateDriftEnd()
    {

    }
    private void StatePushingStart()
    {
        audioSource.PlayOneShot(Push);
    }

}
