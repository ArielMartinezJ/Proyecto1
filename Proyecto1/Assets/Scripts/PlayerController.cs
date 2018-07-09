using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

#region Public Variables
    [Header("\t--Public Variables--")]
    public float movementSpeed = 100;
    public float rotationSpeed = 50;
    public float gravityMultiplier = 1.0f;
    public float gravity = 10.0f;
    public float verticalVelocity;
    public float jumpForce = 7.0f;

    #endregion
    //Player Movement
    private CharacterController characterController;
    private float x_input;
    private float z_input;
    private Vector3 movement;
    private Vector3 moveVector;
    private CollisionFlags colisionesDelPlayer = CollisionFlags.None;


    void Start ()
    {
        characterController = GetComponent<CharacterController>();
	}
	

	void Update ()
    {
        x_input = Input.GetAxis("Horizontal") * movementSpeed;
        z_input = Input.GetAxis("Vertical") * movementSpeed;

        //this.transform.Rotate(0, z_input, 0);
        //this.transform.Translate(z_input, 0, x_input);

        movement = new Vector3(x_input, 0f, z_input);

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
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);

        colisionesDelPlayer = characterController.collisionFlags;

        characterController.Move((movement + moveVector) * Time.deltaTime);
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
            GameManager.Instance.GameOver();
        }

        if (hit.gameObject.tag == "Goal")
        {
            //GameManager.Instance.Victory();
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
