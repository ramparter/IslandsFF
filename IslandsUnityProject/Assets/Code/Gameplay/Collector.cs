using UnityEngine;
using System.Collections;


public class Collector : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollect(Collectable collectable)
    {
        transform.parent.GetComponent<PlayerScript>().OnCollect(collectable);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

    }
}
