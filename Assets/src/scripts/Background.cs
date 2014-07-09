using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
	
	public GameObject background1;
	public GameObject background2;


	public float screenTop;
	public float backgroundCenter1;
	public float backgroundCenter2;
	public float backgroundHeight;

	// Use this for initialization
	void Start () {
		backgroundCenter1 = background1.transform.position.y;
		backgroundCenter2 = background2.transform.position.y;

		backgroundHeight = background1.renderer.bounds.size.y;
	}
	
	// Update is called once per frame
	void Update () {
	
		screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
		int x = GetFirstBackground();
		if (x == 1 && (backgroundCenter2 + backgroundHeight / 2) <= screenTop + 1) {
			backgroundCenter1 = backgroundCenter2 + backgroundHeight;
			background1.transform.position = new Vector3(0f, backgroundCenter1, 5f);
				}
		if (x == 2 && (backgroundCenter1 + backgroundHeight / 2) <= screenTop + 1) {
			backgroundCenter2 = backgroundCenter1 + backgroundHeight;
			background2.transform.position = new Vector3(0f, backgroundCenter2, 5f);
		}
	}

	int GetFirstBackground() {
		if (backgroundCenter1 < backgroundCenter2) {
			return 1;
		} else {
			return 2; 
		}
	}
}
