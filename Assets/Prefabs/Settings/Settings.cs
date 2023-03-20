using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
	private static Settings instance;

	public static float masterVolume = 1.0f;
	public static float bgmVolume = 1.0f;
	public static float soundFXVolume = 1.0f;

	void Awake() {
		if (instance) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	// Update is called once per frame
	void Update() {
		//
	}
}
