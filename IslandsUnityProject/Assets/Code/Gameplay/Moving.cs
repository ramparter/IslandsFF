using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

    public Vector2 speed = new Vector2(-1, 0);
    public float rotation = 0;
    float cameraWidth;
	// Use this for initialization
	void Start () {
        cameraWidth = (Camera.main.ViewportToWorldPoint(Vector2.right).x - Camera.main.transform.position.x) * 2;
        //Debug.Log(cameraWidth);
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main.transform.position.x > transform.position.x + cameraWidth)
            Destroy(gameObject);
	}

    void Move()
    {
        transform.Rotate(transform.forward, rotation * Time.deltaTime);
        if (rigidbody2D)
        {
            rigidbody2D.velocity = speed;
        }
        else
        {
            transform.position += (Vector3)speed * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Move();
    }
}
