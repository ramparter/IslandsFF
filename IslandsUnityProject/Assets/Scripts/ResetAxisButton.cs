using UnityEngine;
using System.Collections;

public class ResetAxisButton : MonoBehaviour {

    //float direction = 1;
	// Use this for initialization
	void Start () 
    {
#if UNITY_ANDROID
#else
        Destroy(gameObject);
#endif
    }
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(Vector3.forward, Time.deltaTime * direction * 10);
	}


    void OnMouseDrag()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().ResetAxis();
    }

    void OnMouseUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().ResetAxis();
    }
}
