using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBoi : MonoBehaviour {
	[SerializeField] private int extraBois = 1;
	[SerializeField] private Collider2D triggerCollider;

	private GameObject[] bois;
	private SpriteRenderer spriteRenderer;
	private Sprite sprite;

	void Awake() {
		if (!triggerCollider) {
			triggerCollider = GetComponent<Collider2D>();
		}

		spriteRenderer = GetComponent<SpriteRenderer>();
		sprite = spriteRenderer.sprite;

		if (extraBois > 1) {
			bois = new GameObject[extraBois - 1];
			Vector3 position;
			SpriteRenderer renderer;

			for (int i = 0, length = extraBois - 1, mid = length >> 1, sortIndex; i < length; i++) {
				sortIndex = (i >= mid) ? (i - mid + 1) : (i - mid);
				bois[i] = new GameObject("boi");
				bois[i].transform.parent = transform;
				position = Vector3.zero;
				position.x = 0.15f * sortIndex;
				bois[i].transform.localPosition = position;
				renderer = bois[i].AddComponent<SpriteRenderer>();
				renderer.sprite = sprite;
				renderer.sortingOrder = (i < mid) ? i : i + 1;
			}

			spriteRenderer.sortingOrder = (extraBois - 1) >> 1;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag("Player")) {
			LivesUI.Lives += extraBois;
			gameObject.SetActive(false);
		}
	}
}
