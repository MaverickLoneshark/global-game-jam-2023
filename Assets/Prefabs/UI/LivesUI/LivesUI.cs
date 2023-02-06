using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class LivesUI : MonoBehaviour {
	[SerializeField] private int startingLives = 2;
	private static TextMeshProUGUI textMeshPro;
	private static int lives;

	public static int Lives {
		get {
			return lives;
		}

		set {
			lives = value;

			if (lives < 0) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}

			textMeshPro.text = "x " + lives;
		}
	}

	void Awake() {
		textMeshPro = transform.Find("Counter").GetComponent<TextMeshProUGUI>();
		Lives = startingLives;
	}

	// Update is called once per frame
	void Update() {
		#if UNITY_EDITOR
			if (Lives != startingLives) {
				Lives = startingLives;
			}
		#endif
	}
}
