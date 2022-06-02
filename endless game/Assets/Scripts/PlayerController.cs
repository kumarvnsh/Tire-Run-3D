using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed; 
    public float maxSpeed;
    private int desiredLane = 1; //0:left 1:middle 2:right
    public float laneDistance = 4; // distancebetween two lanes

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;
    private bool isSliding;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!PlayerManager.isGameStarted)
           return;
        //increase speed   
        if(forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.fixedDeltaTime;
        
        animator.SetBool("isGameStarted", true );   
        direction.z = forwardSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        
        if(controller.isGrounded)
        {
            
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        } else
        {
            direction.y += Gravity * Time.fixedDeltaTime;
        } 
        //Gather the inputs on which we should be
        
        if (SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }

        
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if(desiredLane == 3)
                desiredLane = 2;
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if(desiredLane == -1)
                desiredLane = 0;
        }

        //calculate where we should be in future

        Vector3 targetPosition= transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 50 * Time.fixedDeltaTime); 
        //controller.center = controller.center;

        //OR

        if(transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.fixedDeltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                  controller.Move(moveDir);
            else
                 controller.Move(diff);

        }
        
        
        //Move Player
        controller.Move(direction * Time.deltaTime);
    }

    

    private void Jump()
    {
       direction.y = jumpForce;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }


    public IEnumerator Slide()
    {
        isSliding = true;
       animator.SetBool("isSliding", true);
       controller.center = new Vector3 (0, -0.5f, 0);
       controller.height = 1;

       yield return new WaitForSeconds(1.3f);
       
       controller.center = new Vector3 (0, 0, 0);
       controller.height = 2;
       animator.SetBool("isSliding", false);
       isSliding = false;
    }
}
