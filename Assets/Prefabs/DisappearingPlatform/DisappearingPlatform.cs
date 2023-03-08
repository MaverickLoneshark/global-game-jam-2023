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
	private float lastChangeTime = 0;

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
	}

	private void Start() {
		//Time is not defined until after Awake()!
		lastChangeTime = Time.timeSinceLevelLoad;
	}

	private void Update() {
		//
	}

	void FixedUpdate() {
		if (transform.localPosition != targetPosition) {
			transform.localPosition += (targetPosition - transform.localPosition).normalized * speed;

			if ((targetPosition - transform.localPosition).sqrMagnitude < 0.01f) {
				transform.localPosition = targetPosition;
				lastChangeTime = Time.timeSinceLevelLoad;
				bodyCollider.enabled = (targetPosition == startingPosition);
			}
		}
		else if (Time.timeSinceLevelLoad >= (lastChangeTime + intervalDuration)) {
			if (transform.localPosition == startingPosition) {
				targetPosition = startingPosition + secondPosition;
			}
			else {
				targetPosition = startingPosition;
			}

			bodyCollider.enabled = false;
		}
	}

	private void OnDrawGizmos() {
		if (!meshFilter) {
			meshFilter = GetComponent<MeshFilter>();
		}

		Gizmos.DrawMesh(meshFilter.sharedMesh, transform.position + secondPosition, transform.localRotation, transform.localScale);
	}
}
