using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
	[SerializeField] private Collider2D bodyCollider;

	void Awake() {
		bodyCollider = GetComponent<Collider2D>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Player")) {
			LivesUI.Lives--;
			collision.collider.transform.position = RespawnPoint.instance.transform.position;
		}
	}
}
