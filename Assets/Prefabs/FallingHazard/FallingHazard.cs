using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingHazard : MonoBehaviour {
	[SerializeField] private ContactFilter2D contactFilter;
	[SerializeField] private GameObject objectType;
	[SerializeField] private Vector3 startingPosition = Vector3.up;
	[SerializeField] private Vector3 moveRate = Vector3.zero;
	[SerializeField] private byte maxCollisionChecks = 8;

	private GameObject fallingObject;
	private Hazard hazard;
	private BoxCollider2D triggerCollider;
	private MeshFilter meshFilter;
	private Collider2D objectCollider;
	private Collider2D[] otherColliders;
	private bool triggered = false;

	void Awake() {
		fallingObject = GameObject.Instantiate(objectType, transform);
		fallingObject.transform.position = transform.position + startingPosition;
		hazard = fallingObject.GetComponent<Hazard>();
		if (!hazard) {
			hazard = fallingObject.AddComponent<Hazard>();
		}

		objectCollider = fallingObject.GetComponent<Collider2D>();
		triggerCollider = GetComponent<BoxCollider2D>();
		Vector2 size = triggerCollider.size;
		size.y = startingPosition.y * 0.5f;
		triggerCollider.size = size;
		triggerCollider.offset = Vector2.up * size.y * 0.5f;
		otherColliders = new Collider2D[maxCollisionChecks];

		if (moveRate == Vector3.zero) {
			moveRate = Physics2D.gravity * Time.fixedDeltaTime;
		}
	}

	private void Update() {
		//
	}

	private void FixedUpdate() {
		if (triggered) {
			fallingObject.transform.position += moveRate;

			int collisions = objectCollider.OverlapCollider(contactFilter, otherColliders);

			for (int i = 0; i < collisions; i++) {
				if (!otherColliders[i].CompareTag("Player")) {
					triggered = false;
					triggerCollider.enabled = false;
					hazard.active = false;
				}
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (!triggered) {
			triggered = true;
		}
	}

	private void OnDrawGizmos() {
		if (objectType) {
			if (!(Application.isPlaying && triggered)) {
				if (!meshFilter) {
					meshFilter = objectType.GetComponent<MeshFilter>();
				}

				Gizmos.color = Color.white;
				Gizmos.DrawMesh(meshFilter.sharedMesh, transform.position + startingPosition, Quaternion.identity, transform.localScale);
				Gizmos.color = Color.magenta;
				Gizmos.DrawWireMesh(meshFilter.sharedMesh, transform.position, Quaternion.identity, transform.localScale);
			}
		}
	}
}
