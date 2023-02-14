using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class LivesUI : MonoBehaviour {
	public static LivesUI instance;

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

				instance.remainingLives = lives;
				textMeshPro.text = "x " + lives;
			}
		}
	}

	void Awake() {
		if (!instance) {
			instance = this;
			textMeshPro = transform.Find("Counter").GetComponent<TextMeshProUGUI>();
			Lives = remainingLives;
		}
		else {
			Destroy(gameObject);
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
