using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour {
	[SerializeField] private TMPro.TextMeshProUGUI [] options;

	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject audioMenu;
	[SerializeField] private GameObject inputMenu;

	private int currentOption = 0;
	private float lastVerticalInput = 0;

	void Awake() {
		lastVerticalInput = 0;
		currentOption = 0;
	}

	// Update is called once per frame
	void Update() {
		if (!audioMenu.activeSelf && !inputMenu.activeSelf) {
			float verticalInput = Input.GetAxisRaw("Vertical");

			if (!mainMenu.activeSelf) {
				mainMenu.SetActive(true);
			}

			if (lastVerticalInput == 0) {
				if (verticalInput > 0) {
					options[currentOption].color = Color.gray;

					if (currentOption > 0) {
						currentOption--;
					}
					else {
						currentOption = options.Length - 1;
					}

					options[currentOption].color = Color.white;
				}
				else if (verticalInput < 0) {
					options[currentOption].color = Color.gray;

					if (currentOption < (options.Length - 1)) {
						currentOption++;
					}
					else {
						currentOption = 0;
					}

					options[currentOption].color = Color.white;
				}
			}

			if (Input.GetKeyDown(KeyCode.Escape)) {
				Time.timeScale = 1.0f;
				SoundManager.PlayBGM();
				gameObject.SetActive(false);
			}
		
			if (Input.GetKeyDown(KeyCode.Return)) {
				switch (currentOption) {
					case 0:
						Time.timeScale = 1.0f;
						SoundManager.PlayBGM();
						gameObject.SetActive(false);
					break;

					case 1:
						audioMenu.SetActive(true);
					break;

					default:
					case 2:
					case 3:
						UnityEngine.SceneManagement.SceneManager.LoadScene(0);
					break;
				}
			}

			lastVerticalInput = verticalInput;
		}
		else if (mainMenu.activeSelf) {
			mainMenu.SetActive(false);
		}
	}
}
