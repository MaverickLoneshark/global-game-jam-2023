using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	[SerializeField] int bgmTrack = 0;

	// Start is called before the first frame update
	void Start() {
		SoundManager.ChangeToTrack(bgmTrack);
		SoundManager.PlayBGM();
	}

	// Update is called once per frame
	void Update() {
		//
	}
}
