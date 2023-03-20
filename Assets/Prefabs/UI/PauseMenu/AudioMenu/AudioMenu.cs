using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioMenu : MonoBehaviour {
	[SerializeField] private TMPro.TextMeshProUGUI [] labels;
	[SerializeField] private Slider [] sliders;

	[SerializeField] private int selectedOption = 0;
	private float lastVerticalInput;
	private float lastHorizontalInput;

	void Awake() {
		selectedOption = 0;
		lastVerticalInput = 0;
		lastHorizontalInput = 0;
	}

	private void Start() {
		sliders[0].SetValueWithoutNotify(SoundManager.GetMasterVolume());
		sliders[1].SetValueWithoutNotify(SoundManager.GetBGMVolume());
		sliders[2].SetValueWithoutNotify(SoundManager.GetSoundFXVolume());
	}

	private void OnEnable() {
		if (selectedOption < 2) {
			SoundManager.PlayBGM();
		}
	}

	// Update is called once per frame
	void Update() {
		float verticalInput = Input.GetAxisRaw("Vertical");
		float horizontalInput = Input.GetAxisRaw("Horizontal");

		if (lastVerticalInput == 0) {
			int option = selectedOption;

			if (verticalInput > 0) {
				option--;

				if (option < 0) {
					option = labels.Length - 1;
				}

				SelectOption(option);
			}
			else if (verticalInput < 0) {
				option++;

				if (option >= labels.Length) {
					option = 0;
				}

				SelectOption(option);
			}
		}

		if ((lastHorizontalInput == 0) && (selectedOption < sliders.Length)) {
			if (horizontalInput > 0) {
				sliders[selectedOption].value += 0.05f;
				if (selectedOption != 1) {
					SoundManager.PlaySound(0);
				}
			}
			else if (horizontalInput < 0) {
				sliders[selectedOption].value -= 0.05f;
				if (selectedOption != 1) {
					SoundManager.PlaySound(0);
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape) || ((selectedOption >= sliders.Length) && Input.GetKeyDown(KeyCode.Return))) {
			SoundManager.PauseBGM();
			gameObject.SetActive(false);
		}

		lastVerticalInput = verticalInput;
		lastHorizontalInput = horizontalInput;
	}

	private void SelectOption(int option) {
		labels[selectedOption].color = Color.gray;
		labels[option].color = Color.white;

		//might not need this...?
		if (selectedOption < sliders.Length) {
			sliders[selectedOption].interactable = false;
		}

		if (option < sliders.Length) {
			sliders[option].interactable = true;
		}

		if (option < 2) {
			SoundManager.PlayBGM();
		}
		else {
			SoundManager.PauseBGM();
		}

		selectedOption = option;
	}

	public void UpdateMasterVolume(float volume) {
		SoundManager.UpdateMasterVolume(volume);
	}

	public void UpdateBGMVolume(float volume) {
		SoundManager.UpdateBGMVolume(volume);
	}

	public void UpdateSoundFXVolume(float volume) {
		SoundManager.UpdateSoundFXVolume(volume);
	}
}
