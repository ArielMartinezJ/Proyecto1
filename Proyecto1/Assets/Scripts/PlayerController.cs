using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

#region Public Variables
    [Header("\t--Public Variables--")]
    public float movementSpeed = 10;
    public float rotationSpeed = 5;
    public float gravityMultiplier = 1.0f;
    public float gravity = 10.0f;
    public float verticalVelocity;
    public float jumpForce = 7.0f;

    #endregion
    //Player Movement
    private CharacterController characterController;
    private float x_input;
    private float z_input;
    private float newAngle;
    private Rigidbody myRigidbody;
    private Vector3 movement;
    private Vector3 direction;
    private CollisionFlags colisionesDelPlayer = CollisionFlags.None;

    public Quaternion from = Quaternion.Euler(0f, 0f, 0f);
    public Quaternion to = Quaternion.Euler(0f, 90f, 0f);

    void Start ()
    {
        characterController = GetComponent<CharacterController>();
        myRigidbody = GetComponent<Rigidbody>();

    }
	

	void Update ()
    {
        x_input = Input.GetAxis("Horizontal");
        z_input = Input.GetAxis("Vertical");

        //this.transform.Rotate(0, z_input, 0);
        //this.transform.Translate(z_input, 0, x_input);

        if (characterController.isGrounded)
        {
            
        }
        movement = new Vector3(x_input, 0f, z_input);
        movement = transform.TransformDirection(movement);

        //transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, z_input));

        /*if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.Lerp(from, to, Time.time * rotationSpeed);
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(to, from, Time.time * rotationSpeed);
        }*/



        if (movement != Vector3.zero)
        {
            if (Vector3.Angle(transform.forward, direction) > 179)
            {
                // This will cause us to always turn to the right to go the opposite direction
                direction = transform.TransformDirection(new Vector3(.01f, 0, -1));
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 90f * Time.deltaTime);
            //transform.rotation = Quaternion.LookRotation(movement);
            //Quaternion.Slerp(transform.rotation, (transform.rotation * to), rotationSpeed * Time.deltaTime);
            //Quaternion rotation = Quaternion.LookRotation((transform.position + this.myRigidbody.velocity) - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            Vector3 moveDirection = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;


            movement *= movementSpeed;
            characterController.Move((movement) * Time.deltaTime);
        }
        /*else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }*/
        //transform.LookAt(movement, Vector3.up);

        //direction = movement;
        //direction = new Vector3(x_input, 0f, z_input);

        /*newAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);*/

        if (characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            /*if (Input.GetAxis("Jump") > 0)
            {
                verticalVelocity = jumpForce;
            }*/
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        //Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        /*Vector3 moveDirection = transform.forward;
        moveDirection.y -= gravity * Time.deltaTime;
        colisionesDelPlayer = characterController.collisionFlags;

        movement *= movementSpeed;
        characterController.Move((movement + moveDirection) * Time.deltaTime);*/
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (colisionesDelPlayer == CollisionFlags.Below)
            return;

        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        if (hit.gameObject.tag == "Enemy")
        {
            GameManager.Instance.ShowDefeatScreen();
        }

        if (hit.gameObject.tag == "Goal")
        {
            GameManager.Instance.ShowVictoryScreen();
        }
        //body.AddForceAtPosition(-hit.normal * weight, hit.point);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.GameOver();
        }
    }*/
}
