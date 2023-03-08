using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class LivesUI : MonoBehaviour {
	private static LivesUI instance;

	[SerializeField] private int remainingLives = 2;

	private static TextMeshProUGUI textMeshPro;
	private static int lives;

	public static int Lives {
		get {
			return lives;
		}

		set {
			lives = value;

			if (instance) {
				if (lives < 0) {
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				}
				else {
					instance.remainingLives = lives;
					textMeshPro.text = "x " + lives;
					
					if (SoundManager.Ready) {
						SoundManager.SetBGMPitch((lives < 2) ? 1.5f - (lives * 0.25f) : 1.0f);
					}
				}
			}
		}
	}

	void Awake() {
		if (instance) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			textMeshPro = transform.Find("Counter").GetComponent<TextMeshProUGUI>();
			Lives = remainingLives;
		}
	}

	// Update is called once per frame
	void Update() {
		#if UNITY_EDITOR
			if (remainingLives != Lives) {
				Lives = remainingLives;
			}
		#endif
	}
}
