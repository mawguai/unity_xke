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
    public string vertical;

    private Rigidbody rb;
    private bool rising;
    private bool jumping;
    private Animator anim;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = 2;
        jumping = transform.position.y != 0;
	    if (Input.GetButtonDown(vertical) && !jumping)
        {
            Vector3 jumpv = new Vector3(0, jumpSpeed, 0);
            rb.AddForce(jumpv);
        }

        float h = Input.GetAxisRaw(horizontal);
        float v = Input.GetAxisRaw(vertical);

        Animating(h, v);
    }

    private void FixedUpdate()
    {
        rising = rb.velocity.y >= 0;
        if (Input.GetButton(vertical) && transform.position.y < boundary.yMax && rising)
        {
            Vector3 jumpv = new Vector3(0, jumpSpeed, 0);
            rb.AddForce(jumpv);
        }
        if (Input.GetButton(horizontal))
        {
            float moveHorizontal = Input.GetAxisRaw(horizontal);
            rb.velocity = transform.up * rb.velocity.y + transform.right * moveHorizontal * runSpeed;
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, boundary.xMin, boundary.xMax);
            transform.position = position;
        } else
        {
            rb.velocity = transform.up * rb.velocity.y;
        }
    }

    void Animating(float h, float v)
    {
        bool moving = h != 0f || v != 0f;
        anim.SetBool("IsMoving", moving);
        anim.speed = 0.8f;
    }
}
