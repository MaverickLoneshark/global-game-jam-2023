using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
	public bool active = true;
	[SerializeField] private Collider2D bodyCollider;
	MeshRenderer meshRenderer;

	void Awake() {
		bodyCollider = GetComponent<Collider2D>();
		meshRenderer = GetComponentInChildren<MeshRenderer>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (active) {
			if (collision.collider.CompareTag("Player")) {
				Color color = meshRenderer.material.color;
				color.r -= 0.25f;
				color.g += 0.25f;
				color.b -= 0.25f;
				meshRenderer.material.color = color;
				SoundManager.PlaySound(1);
				LivesUI.Lives--;
				collision.gameObject.GetComponent<bokidController>().Respawn();
			}
		}
	}
}
