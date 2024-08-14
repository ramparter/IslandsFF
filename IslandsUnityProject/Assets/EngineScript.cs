using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineScript : MonoBehaviour
{
    public Transform exhaustPrfab; 
    float exhaustRate = 0.25f;
    private float t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        if(t > exhaustRate)
        {
            t = 0;
            var exhaustTransform = Instantiate(exhaustPrfab) as Transform;
            exhaustTransform.position = transform.position;
            exhaustTransform.rotation = transform.rotation;

            MoveScript move = exhaustTransform.gameObject.GetComponent<MoveScript>();
            if(move != null ) 
                move.speed = GetComponentInParent<PlayerScript>().Speed / 2;
        }
    }
}
