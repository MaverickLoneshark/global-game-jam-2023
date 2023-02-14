using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPower : MonoBehaviour {
	[SerializeField] private bool infiniteRespawns = false;
	[SerializeField] private string message = "Respawn Power\nPress 'R' to respawn";

	bool powerEnabled = false;
	SpriteRenderer spriteRenderer;
	Collider2D triggerCollider;

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		triggerCollider = GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void Update() {
		if (powerEnabled) {
			if (Input.GetKeyDown(KeyCode.R)) {
				if (!infiniteRespawns) {
					LivesUI.Lives--;
				}

				transform.parent.position = RespawnPoint.instance.transform.position;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			transform.parent = collision.transform;
			transform.localPosition = Vector3.zero;
			powerEnabled = true;
			spriteRenderer.enabled = false;
			triggerCollider.enabled = false;

			if (infiniteRespawns) {
				MessageUI.Message = message;
			}
			else {
				MessageUI.Message = message + " (costs one life)";
			}
		}
	}
}
