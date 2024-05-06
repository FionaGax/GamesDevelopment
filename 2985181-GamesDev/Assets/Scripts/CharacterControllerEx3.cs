using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterControllerScriptEx3 : MonoBehaviour
{
    public float maxSpeed;
    public float accelerationForce;
    public Rigidbody2D myRigidbody;
    public float jumpForce;
    
    public bool isOnGround;
    public float secondaryJumpForce;
    public float secondaryJumpTime;
    public bool secondaryJump;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>(); // Look for a component called Rigidbody2D and assign it to variable
        anim = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(myRigidbody.velocity.x));

        // Animation flip code
        if (Input.GetAxis("Horizontal") > 0.1f) // If the player is moving right
        {
            anim.transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") < -0.1f) // if the player is moving to the left
        {
            anim.transform.localScale = new Vector3(-1, 1, 1); // set the scale of the player to -1,1,1
        }

        // Animation flip code end

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && Mathf.Abs(myRigidbody.velocity.x) < maxSpeed) // If the absolute value of the input is greater than 0.1 and the player is not moving faster than max speed
        {
            // Gets input value and multiplies it by acceleration in the x direction
            myRigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * accelerationForce, 0), ForceMode2D.Force);
        }
        
        // Jumping code
        if (isOnGround && Input.GetButtonDown("Jump"))
        {
            myRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            StartCoroutine(SecondaryJump());
        }
        if (isOnGround == false && Input.GetButton("Jump") && secondaryJump == true)
        {
            myRigidbody.AddForce(new Vector2(0, secondaryJumpForce), ForceMode2D.Force); // While the jump button is held
        }
    }
    private void OnTriggerStay2D(Collider2D other) // As long as the collider is detected inside the trigger, the player will register as being on the ground
    {
        isOnGround = true;
    }
    private void OnTriggerExit2D(Collider2D other) // When the collider exits the trigger, the player will no longer register as on the ground
    {
        isOnGround = false;
    }
    IEnumerator SecondaryJump()
    {
        secondaryJump = true;
        yield return new WaitForSeconds(secondaryJumpTime); // wait for a certain amount of time
        secondaryJump = false;
    }
}