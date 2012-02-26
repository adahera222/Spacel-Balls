using UnityEngine;

public sealed class AudioManager {
	
	#region Fields
	private static AudioManager _instance = new AudioManager();
	
	private AudioSource _bgmSource;
	private AudioSource _sfxSource;
	#endregion
	
	#region Properties
	public float SFXVolume {
		get { return _sfxSource.volume; }
		set { _sfxSource.volume = value; }
	}
	
	public float BGMVolume {
		get { return _bgmSource.volume; }
		set { _bgmSource.volume = value; }
	}
	#endregion
	
	public static AudioManager GetInstance(){
		return _instance;
	}
	
	public void StartUp(){
		Camera c = Camera.mainCamera;
		
		_bgmSource = c.gameObject.AddComponent<AudioSource>();
		_bgmSource.volume = BGMVolume;
		_bgmSource.playOnAwake = false;
		
		_sfxSource = c.gameObject.AddComponent<AudioSource>();
		_sfxSource.volume = SFXVolume;
		_sfxSource.playOnAwake = false;
	}
	
	public void PlayBGM(AudioClip clip) {
		if (_bgmSource.isPlaying) {
			_bgmSource.Stop();
		}
		
		_bgmSource.clip = clip;
		_bgmSource.Play();
	}
	
	public void PlaySFX(AudioClip clip){
		_sfxSource.PlayOneShot(clip);
	}
}