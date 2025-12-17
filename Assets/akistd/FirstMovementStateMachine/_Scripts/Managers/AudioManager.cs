using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace akistd
{
    public class AudioManager : MonoBehaviour
    {
		[SerializeField]
        private AudioSource SFX_AudioSource;
		[SerializeField]
		private AudioSource Music_AudioSource;

        // Random pitch adjustment range.
        public float LowPitchRange = .95f;
        public float HighPitchRange = 1.05f;

        public static AudioManager Instance = null;

        private AudioMixer AllMixer;

        public AudioMixer GeneralMixer { get => AllMixer; }

        private void Awake()
		{

            // If there is not already an instance of SoundManager, set it to this.
            if (Instance == null)
			{
                AllMixer = Music_AudioSource.outputAudioMixerGroup.audioMixer;
                Instance = this;
			}
			//If an instance already exists, destroy whatever this object is to enforce the singleton.
			else if (Instance != this)
			{
				Destroy(gameObject);
                
            }

			//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
			DontDestroyOnLoad(gameObject);
		}

        private void Start()
        {

        }

        // Play a single clip through the sound effects source.
        public void Play(AudioClip clip)
		{
            //SFX_AudioSource.clip = clip;
			SFX_AudioSource.PlayOneShot(clip);
		}

        public void StopSfx()
        {
            SFX_AudioSource.Stop();
        }
		// Play a single clip through the music source.
		public void PlayMusic(AudioClip clip)
		{
			Music_AudioSource.clip = clip;
            Music_AudioSource.Play();
        }

		// Play a random clip from an array, and randomize the pitch slightly.
		public void RandomSoundEffect(List<AudioClip> clips, float speed=.95f, akistd.FirstPerson.PlayerMovementAudioData.AudioCate audioType= FirstPerson.PlayerMovementAudioData.AudioCate.Footstep)
		{
            int randomIndex = Random.Range(0, clips.Count);
            float randomPitch;
            if (audioType == FirstPerson.PlayerMovementAudioData.AudioCate.Footstep)
            {
                if (!SFX_AudioSource.isPlaying)
                {
                    
                    if (speed != LowPitchRange)
                    {
                        randomPitch = speed;

                    }
                    else
                    {
                        randomPitch = Random.Range(LowPitchRange, HighPitchRange);
                    }

                    SFX_AudioSource.pitch = randomPitch;
                    SFX_AudioSource.clip = clips[randomIndex];
                    SFX_AudioSource.PlayOneShot(SFX_AudioSource.clip);
                }
            }
            else
            {
                /*if (speed != LowPitchRange)
                {
                    randomPitch = speed;

                }
                else
                {
                    randomPitch = Random.Range(LowPitchRange, HighPitchRange);
                }

                SFX_AudioSource.pitch = randomPitch;
                SFX_AudioSource.clip = clips[randomIndex];*/
                SFX_AudioSource.PlayOneShot(clips[randomIndex]);
            }
            
            


        }

		public void lofi()
        {
            AudioMixer mixer = Music_AudioSource.outputAudioMixerGroup.audioMixer;
            mixer.SetFloat("cutoff", 380f);
        }

        public void clearEffect()
        {
            AudioMixer mixer = Music_AudioSource.outputAudioMixerGroup.audioMixer;
            mixer.SetFloat("cutoff", 5000.00f);
            
        }

        public void changeMainVolume(float amount)
        {
            AudioMixer mixer = Music_AudioSource.outputAudioMixerGroup.audioMixer;
            AllMixer = mixer;
            AllMixer.SetFloat("MainVolume", amount);
            GameManager.Instance.SaveGameSettings();
        }

        public void changeMusicVolume(float amount)
        {
            AudioMixer mixer = Music_AudioSource.outputAudioMixerGroup.audioMixer;
            AllMixer = mixer;
            AllMixer.SetFloat("MusicVolume", amount);
            GameManager.Instance.SaveGameSettings();
        }

        public void changeSFXVolume(float amount)
        {
            AudioMixer mixer = Music_AudioSource.outputAudioMixerGroup.audioMixer;
            AllMixer = mixer;
            AllMixer.SetFloat("SFXVolume", amount);
            GameManager.Instance.SaveGameSettings();

        }
    }
}
