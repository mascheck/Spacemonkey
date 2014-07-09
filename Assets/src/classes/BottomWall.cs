using UnityEngine;
using System.Collections;

public class BottomWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.layer == 10)
        {
            Destroy(coll.gameObject);
        }
	}
}
