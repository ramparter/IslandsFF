using UnityEngine;
using System.Collections;

public class CollectableContainer : MonoBehaviour {

    public Transform collectable;
    public float chance;

    private bool containsCollectable;

	// Use this for initialization
	void Start ()
    {
        if (chance >= Random.value)
        {
            containsCollectable = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (containsCollectable && gameObject.GetComponent<HealthScript>() && !gameObject.GetComponent<HealthScript>().Alive)
        {
            var spawned = Instantiate(collectable) as Transform;
           // Instantiate(collectable);
            spawned.position = transform.position;
            //Debug.Log(collectable.position + " " + transform.position);
            containsCollectable = false;
        }

	}


}
