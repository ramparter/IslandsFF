using UnityEngine;
using System.Collections;

public class AxisDot : MonoBehaviour {

    PlayerScript player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	    transform.localPosition = AccelReader.GetAccelMovement() * 1.8f;
	}
}
