using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	private static SoundManager instance;

	public static bool Ready {
		get {
			return instance != null;
		}
	}

	//consider decoupling sources from player
	[SerializeField] private int currentBGMTrack = 0;
	[SerializeField] private AudioClip [] bgmClips;
	[SerializeField] private AudioClip [] soundClips;

	[SerializeField] private int polyphony = 8;
	[SerializeField] private AudioSource bgmPlayer;
	[SerializeField] private AudioSource[] soundPlayer;

	void Awake() {
		if (instance) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			bgmPlayer = gameObject.AddComponent<AudioSource>();
			bgmPlayer.playOnAwake = false;
			bgmPlayer.loop = true;

			soundPlayer = new AudioSource[polyphony];
			for (int i = 0; i < polyphony; i++) {
				soundPlayer[i] = gameObject.AddComponent<AudioSource>();
				soundPlayer[i].playOnAwake = false;
				soundPlayer[i].loop = false;
			}
		}
	}

	// Update is called once per frame
	void Update() {
		//
	}

	public static void NextTrack() {
		if (instance.currentBGMTrack < (instance.bgmClips.Length - 1)) {
			instance.currentBGMTrack++;
		}
		else {
			instance.currentBGMTrack = 0;
		}

		instance.bgmPlayer.clip = instance.bgmClips[instance.currentBGMTrack];
	}

	public static void PreviousTrack() {
		if (instance.currentBGMTrack > 0) {
			instance.currentBGMTrack--;
		}
		else {
			instance.currentBGMTrack = instance.bgmClips.Length - 1;
		}

		instance.bgmPlayer.clip = instance.bgmClips[instance.currentBGMTrack];
	}

	public static void ChangeToTrack(int track) {
		if ((track >= 0) && (track < instance.bgmClips.Length)) {
			instance.currentBGMTrack = track;
			instance.bgmPlayer.clip = instance.bgmClips[instance.currentBGMTrack];
		}
		else {
			Debug.LogError("Track number is out of bounds");
		}
	}

	public static void PlayBGM() {
		instance.bgmPlayer.Play();
	}

	public static void PauseBGM() {
		instance.bgmPlayer.Pause();
	}

	public static void StopBGM() {
		instance.bgmPlayer.Stop();
	}

	public static void SetBGMPitch(float pitch) {
		if (instance) {
			instance.bgmPlayer.pitch = pitch;
		}
		else {
			Debug.LogError("SoundManager isn't initialized yet...");
		}
	}

	public static void PlaySound(int sound) {
		if ((sound >= 0) && (sound < instance.bgmClips.Length)) {
			for (int i = 0, length = instance.soundPlayer.Length; i < length; i++) {
				if (!instance.soundPlayer[i].isPlaying) {
					instance.soundPlayer[i].clip = instance.soundClips[sound];
					instance.soundPlayer[i].Play();
					break;
				}
			}
		}
		else {
			Debug.LogError("Sound number is out of bounds");
		}
	}
}
