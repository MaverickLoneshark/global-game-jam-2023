using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLift : MonoBehaviour {
	[SerializeField] private GameObject liftType;
	[SerializeField] private float speed = 1.0f;
	[SerializeField] private Vector3[] startingPositions = new Vector3[2];

	private GameObject[] lifts;
	private int[] nextPosition;

	void Awake() {
		int length = startingPositions.Length;
		lifts = new GameObject[length];
		nextPosition = new int[length];

		for (int i = 0; i < length; i++) {
			lifts[i] = Instantiate(liftType, transform);
			lifts[i].transform.localPosition = startingPositions[i];
		}

		nextPosition[length - 1] = length - 1;
		lifts[length - 1].transform.localPosition = startingPositions[length - 1];


		for (int i = 0; i < length - 1; i++) {
			lifts[i].transform.localPosition = startingPositions[i];
			nextPosition[i] = i + 1;
		}
	}

	void FixedUpdate() {
		for (int i = 0, length = startingPositions.Length; i < length; i++) {
			lifts[i].transform.localPosition += (startingPositions[nextPosition[i]] - lifts[i].transform.localPosition).normalized * speed;

			if ((lifts[i].transform.localPosition - startingPositions[nextPosition[i]]).sqrMagnitude < 0.1f) {
				lifts[i].transform.localPosition = startingPositions[nextPosition[i]];

				if (nextPosition[i] < (length - 1)) {
					nextPosition[i]++;
				}
				else {
					nextPosition[i] = 1;
					lifts[i].transform.localPosition = startingPositions[0];
				}
			}
		}
	}

	private void OnDrawGizmos() {
		if (lifts == null) {
			Gizmos.color = Color.white;
			Mesh mesh = liftType.GetComponent<MeshFilter>().sharedMesh;

			for (int i = 0, length = startingPositions.Length; i < length; i++) {
				Gizmos.DrawMesh(mesh, transform.position + startingPositions[i], liftType.transform.rotation, liftType.transform.localScale);
			}
		}
	}
}
