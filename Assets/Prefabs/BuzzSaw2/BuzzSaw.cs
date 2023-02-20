using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzSaw : MonoBehaviour {
	[SerializeField] private MeshRenderer meshRenderer;

	void Awake() {
		if (!meshRenderer) {
			meshRenderer = transform.Find("Model").GetComponent<MeshRenderer>();
		}
	}

	// Update is called once per frame
	void Update() {
		//
	}

	public void TurnGreener() {
		Color color = meshRenderer.material.color;

		if (color.r > 0) {
			color.r -= 0.25f;
		}

		if (color.b > 0) {
			color.b -= 0.25f;
		}

		if (color.g < 1.0f) {
			color.g += 0.25f;
		}

		meshRenderer.material.color = color;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Player")) {
			TurnGreener();
		}
	}
}
