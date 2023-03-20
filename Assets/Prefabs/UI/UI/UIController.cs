using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
	[SerializeField] private GameObject pauseMenu;

	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) {
			if (!pauseMenu.activeSelf) {
				pauseMenu.SetActive(true);
				Time.timeScale = 0;
				SoundManager.PauseBGM();
			}
		}
	}
}
