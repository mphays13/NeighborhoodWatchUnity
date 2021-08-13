using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] float turnSpeed = 70.0f;

    private Rigidbody playerRB;

    private float forwardInput;
    private float horizontalInput;
    private float jumpForce = 200.0f;

    public bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.LeftShift))
            speed = 25;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed = 10;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
