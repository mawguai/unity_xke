using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMax;
}

public class PlayerController : MonoBehaviour {

    public int runSpeed;
    public int jumpSpeed;
    public string horizontal;
    public Boundary boundary;

    private Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private void FixedUpdate()
    {
        if (Input.GetButton(horizontal))
        {
            float moveHorizontal = Input.GetAxisRaw(horizontal);
            rb.velocity = transform.right * moveHorizontal * runSpeed;
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, boundary.xMin, boundary.xMax);
            transform.position = position;
        } else
        {
            rb.velocity = new Vector3(0,0,0);
        }
    }
}
