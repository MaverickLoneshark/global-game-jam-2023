using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageUI : MonoBehaviour {
	public static MessageUI instance;
	[SerializeField] private TMPro.TextMeshProUGUI textMeshProUGUI;
	[SerializeField] private float duration = 2.0f;
	[SerializeField] private float fadeDuration = 2.0f;
	[SerializeField] private string nextString;
	private float endTime;

	public static string Message {
		get {
			return instance ? instance.textMeshProUGUI.text : "No Message.instance available";
		}

		set {
			if (instance) {
				if (Time.timeSinceLevelLoad >= (instance.endTime + instance.fadeDuration)) {
					instance.textMeshProUGUI.text = value;
					Color color = instance.textMeshProUGUI.color;
					color.a = 1f;
					instance.textMeshProUGUI.color = color;
					instance.endTime = Time.timeSinceLevelLoad + instance.duration;
				}
				else {
					instance.nextString = value;
				}
			}
			else {
				Debug.LogError("No Message.instance available");
			}
		}
	}

	void Awake() {
		if (!instance) {
			instance = this;

			if (!textMeshProUGUI) {
				textMeshProUGUI = GetComponent<TMPro.TextMeshProUGUI>();
			}

			endTime = -duration;
		}
		else {
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update() {
		if (Time.timeSinceLevelLoad > endTime) {
			float fade_time = endTime + fadeDuration;

			if (Time.timeSinceLevelLoad < fade_time) {
				Color color = textMeshProUGUI.color;
				color.a = (fade_time - Time.timeSinceLevelLoad) / fadeDuration;
				textMeshProUGUI.color = color;
			}
			else if (textMeshProUGUI.color.a > 0) {
				Color color = textMeshProUGUI.color;
				color.a = 0;
				textMeshProUGUI.color = color;

				if (nextString != null) {
					Message = nextString;
					nextString = null;
				}
			}
		}
	}
}
