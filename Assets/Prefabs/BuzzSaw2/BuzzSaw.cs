using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for BuzzSaw-specific behavior
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
}
