using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

#region Public Variables
    [Header("\t--Public Variables--")]
    public float movementSpeed = 100;
    public float rotationSpeed = 50;


    #endregion
    //Player Movement
    private CharacterController characterController;
    private float x_input;
    private float z_input;
    private Vector3 movement;
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

        colisionesDelPlayer = characterController.collisionFlags;

        characterController.Move(movement * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (colisionesDelPlayer == CollisionFlags.Below)
            return;

        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        //body.AddForceAtPosition(-hit.normal * weight, hit.point);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.GameOver();
        }
    }
}
