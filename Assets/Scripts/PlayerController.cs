using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    //[SerializeField] float turnSpeed = 70.0f;

    public Vector2 turn;
    public float sensitivity = 100f;

    private Rigidbody playerRB;

    private float forwardInput;
    private float horizontalInput;
    private float jumpForce = 200.0f;

    public bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        //This locks the cursor in the center of the screen and hides the mouse
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);


        //transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        //This uses the input for the mouse in order for you to rotate the character
        turn.x += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);


        //This line of code lets the game know if the player is actually touching the ground, and if so it allows you to jump when pressing the space key
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        //The If statements control when the player is either sprinting or not upon holding or or depressing the L Shift key
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
