using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour {
	[SerializeField] private bool startAtSecondPosition = false;
	[SerializeField] private float intervalDuration = 5.0f;
	[SerializeField] private float speed = 1.0f;
	[SerializeField] private Vector3 secondPosition;

	private MeshFilter meshFilter;
	private Collider2D bodyCollider;
	private Vector3 startingPosition;
	private Vector3 targetPosition;
	private float lastChangeTime;

	void Awake() {
		bodyCollider = GetComponent<Collider2D>();
		startingPosition = transform.localPosition;

		if (startAtSecondPosition) {
			transform.localPosition = startingPosition + secondPosition;
			targetPosition = startingPosition;
		}
		else {
			targetPosition = startingPosition + secondPosition;
		}

		lastChangeTime = Time.timeSinceLevelLoad;
	}

	void FixedUpdate() {
		if (Time.timeSinceLevelLoad >= (lastChangeTime + intervalDuration)) {
			if (transform.localPosition == startingPosition) {
				targetPosition = startingPosition + secondPosition;
			}
			else {
				targetPosition = startingPosition;
			}

			lastChangeTime = Time.timeSinceLevelLoad;
		}

		if (transform.localPosition != targetPosition) {
			transform.localPosition += (targetPosition - transform.localPosition).normalized * speed;

			if ((targetPosition - transform.localPosition).sqrMagnitude < 0.01f) {
				transform.localPosition = targetPosition;
			}

			bodyCollider.enabled = (targetPosition == startingPosition);
		}
	}

	private void OnDrawGizmos() {
		if (!meshFilter) {
			meshFilter = GetComponent<MeshFilter>();
		}

		Gizmos.DrawMesh(meshFilter.sharedMesh, transform.position + secondPosition, transform.localRotation, transform.localScale);
	}
}
