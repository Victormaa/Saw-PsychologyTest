using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {
	
	public AudioMixerSnapshot defaultMasterMixerSnapshot;
	
	public AudioSource musicSource1;
	public AudioSource musicSource2;
	[HideInInspector] public AudioSource musicSource;
	public static MusicManager instance = null;

	private float musicLoopingPoint = 0;

	private int stopMusicSmooth = 0;
	// Use this for initialization
	void Awake () {
		//Init Snapshots to avoid the click noise
		defaultMasterMixerSnapshot.TransitionTo (0.01f);
		//2 Channels for smooth transform
		musicSource = musicSource1;

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
			Destroy (gameObject);

		musicSource1.priority = 0;
		musicSource2.priority = 0;
	}

	void Update()
	{
		//Looping Music
		if (musicSource.time > musicLoopingPoint) {
			if(musicSource == musicSource1)
			{
				musicSource = musicSource2;
				
				musicSource.clip = musicSource1.clip;
				musicSource.Play ();
			}
			else
			{
				musicSource = musicSource1;
				
				musicSource.clip = musicSource2.clip;
				musicSource.Play ();
			}
		}
		if(stopMusicSmooth == 1){
			musicSource.volume -= 0.6f * Time.deltaTime;
			if(musicSource.volume <= 0){
				stopMusicSmooth = 0;
				musicSource.volume = 1;
				StopMusic();
			}
		}
	}

	public void PlayMusic(AudioClip clip, float timingPoint = 0)
	{
		musicSource.clip = clip;
		musicSource.Play ();
		musicSource.volume = 1;
		musicLoopingPoint = timingPoint;
	}

	public void StopMusic()
	{
		musicSource1.Stop ();
		musicSource2.Stop ();
		musicSource.clip = null;
	}

	public void StopMusicSmooth(){
		stopMusicSmooth = 1;
	}
}
