using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard : MonoBehaviour {
	[SerializeField] private GameObject hazardObject;
	[SerializeField] private Vector3 [] positions = new Vector3[1];
	[SerializeField] private float moveRate = 1.0f;
	[SerializeField] private bool triggered = true;

	private GameObject movingObject;
	private Hazard hazard;
	private MeshFilter meshFilter;
	private Vector3 nextPosition;
	private byte nextIndex;

	void Awake() {
		movingObject = GameObject.Instantiate(hazardObject, transform);
		movingObject.transform.localPosition = Vector3.zero;
		hazard = movingObject.GetComponent<Hazard>();

		if (!hazard) {
			hazard = movingObject.AddComponent<Hazard>();
		}

		nextIndex = 0;
		nextPosition = positions[nextIndex];
	}

	private void FixedUpdate() {
		if (triggered) {
			Vector3 targetPosition = transform.position + nextPosition;
			movingObject.transform.position += (targetPosition - movingObject.transform.position).normalized * moveRate;

			if ((targetPosition - movingObject.transform.position).sqrMagnitude < 0.01f) {
				movingObject.transform.position = targetPosition;

				if (nextIndex < positions.Length) {
					nextIndex++;
				}
				else {
					nextIndex = 0;
				}

				if (nextIndex != positions.Length) {
					nextPosition = positions[nextIndex];
				}
				else {
					nextPosition = Vector3.zero;
				}
			}
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.white;

		if (!meshFilter && hazardObject) {
			meshFilter = hazardObject.GetComponentInChildren<MeshFilter>();
		}

		if (!Application.isPlaying && meshFilter) {
			Gizmos.DrawMesh(meshFilter.sharedMesh, transform.position, meshFilter.transform.rotation, meshFilter.transform.localScale);
		}

		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, transform.position + positions[0]);

		for (int i = 1, length = positions.Length; i < length; i++) {
			Gizmos.DrawLine(transform.position + positions[i - 1], transform.position + positions[i]);
		}
	}
}
