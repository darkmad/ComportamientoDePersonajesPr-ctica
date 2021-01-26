using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //controlador del personaje
    public CharacterController controller;
    //gravedad
    public float gravity = -9.8f;
    //distancia de salto
    public float jumpHeight = 3f;

    private float originalHeight;
    [SerializeField]private float downHeight;

    //Vector velocidad
    Vector3 velocity;

    //Gestión de la velocidad, tanto la normal como la de sprint.
    public float speed = 12f;

    //Variable para gestionar el salto, y si el personaje está o no en el suelo.
    public Transform groundcheck;
    public float groundDistance = 0.7f;
    public LayerMask groundMask;
    public bool isGrounded = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);


        if (isGrounded && velocity.y <= 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right*x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime); 

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = Mathf.Lerp(controller.height, downHeight, 0.3f);
        }
        else
        {
            controller.height = Mathf.Lerp(controller.height, originalHeight, 0.3f);
        }

        velocity.y += gravity * Time.deltaTime;     
        controller.Move(velocity * Time.deltaTime);

    }



    

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }
}
