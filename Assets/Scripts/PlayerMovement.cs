using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform trans;

    private float speed = 8f;
    private float runSpeed = 15f;
    private float walkSpeed = 8f;
    private float slideSpeed = 0;
    bool isWalking = true;
    bool isRuning;
    bool isSliding;
    bool isCrouching;

    public float gravity = -9.82f;
    public float jumpHeight = 3f;
    public float normalHeight = 3.6f;
    private float jumpHeightOnWall = 2f;
    [HideInInspector] public float AirJumpsReamaining;
    public float JumpsInAir = 1;

    public bool godmode = false;
    // float godmodeY = 1;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public Transform LeftSide;
    public Transform RightSide;
    public LayerMask Walls;
    public LayerMask climableWalls;

    
    public Vector3 velocity;
    bool isGrounded;
    private Animator animator;
    float timer;
    float MaxTimeOnWall = 1.4f;
    public Camera camerak;
    public GameObject gun;
    private float walljumpTimer;
    private float wallvelTimer;
    [HideInInspector] public string wichanim;
    Restart restart;

    void Awake()
    {
        // animator = camera.GetComponent<Animator>();
        // animator = gun.GetComponent<Animator>();
        timer = MaxTimeOnWall;
        restart = FindObjectOfType<Restart>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (godmode == true)
        {
            controller.detectCollisions = false;
            Vector3 movexy = Vector3.Normalize(transform.right * x + transform.forward * z);

            if (Input.GetKey(KeyCode.Space))
            {
                movexy.y += 50 * Time.deltaTime;
                // trans.position = new Vector3(transform.position.x, transform.position.y + 1 * Time.deltaTime, transform.position.z);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                movexy.y -= 50 * Time.deltaTime;

            }
            controller.Move(movexy * speed * Time.deltaTime);
            return;
        }
        controller.detectCollisions = true;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 1")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                restart.restart();
            }
        }

        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     godmode = !godmode;
        // }
        // else if (Input.GetKeyUp(KeyCode.Y))
        // {
        //     gravity = -29.46f;
        //     speed = walkSpeed;
        // }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            AirJumpsReamaining = JumpsInAir;
        }
        // weaponSideAnim(x);

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        bool leftSide = Physics.CheckSphere(LeftSide.position, .1f, Walls);
        bool rightSide = Physics.CheckSphere(RightSide.position, .1f, Walls);

        bool jump = Input.GetButtonDown("Jump");
        if (jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            AirJumpsReamaining = JumpsInAir;
        }
        else if (jump && !isGrounded && AirJumpsReamaining >= 1)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            AirJumpsReamaining--;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // checking if LeftShift is pressed
        // when pressed set isRuning to true and iswalking to false
        if (Input.GetKey(KeyCode.LeftShift) && z>0)
        {
            speed = runSpeed + slideSpeed;
            isRuning = true;
            isWalking = false;
            isCrouching = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || z<0)
        {
            speed = walkSpeed;
            isRuning = false;
            isWalking = true;
            isCrouching = false;
            isSliding = false;
        }

        // if Runing...
        if (isRuning)
        {
            //weapon  shake
            if (z!=0)
            {
                wichanim = "Base Layer.Weapon Running";
            }
            else
            {
                wichanim = "Base Layer.empty";
            }
            
            if (leftSide || rightSide)
            {
                AirJumpsReamaining = 0;
                timer -= Time.deltaTime;
                // If timer runs out player will fall down from the wall
                if (timer <0) {
                    velocity.y = -10f;
                    timer = 0;
                    camerak.transform.localRotation = Quaternion.Euler(0, 0, 0);
                } else {
                    //Checking if time since startup is bigger than (itself + float)
                    if ((Time.realtimeSinceStartup > walljumpTimer) && jump && Input.GetKey(KeyCode.W))
                    {
                        walljumpTimer = Time.realtimeSinceStartup + 0.75f;
                        wallvelTimer = Time.realtimeSinceStartup + 0.5f;
                        velocity.y = Mathf.Sqrt(jumpHeightOnWall * -2 * gravity);
                        timer = MaxTimeOnWall;
                    }
                    else if (Time.realtimeSinceStartup > wallvelTimer)
                    {
                        RaycastHit hit;
                        bool groundBelow = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 4.25f, groundMask);
                        if (!groundBelow)
                        {
                            velocity.y = -1f;
                            // Debug.Log("not groundBelow");
                            if (rightSide) {
                                camerak.transform.localRotation = Quaternion.Euler(0, 0, 15);
                            }
                            else if (leftSide) {
                                camerak.transform.localRotation = Quaternion.Euler(0,0,-15);
                            }   
                        }
                    }
                }
                
                wichanim = "Base Layer.empty";
            }
            else {
                timer = MaxTimeOnWall;
                camerak.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            //Checking if C is pressed while running
            if (Input.GetKeyDown(KeyCode.C))
            {
                isSliding = true;
                slideSpeed = 5f;
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                isSliding = false;
            }
            //checking if Sliding
            if (isSliding)
            {
                slideSpeed -= 9*Time.deltaTime;
                controller.height = 2.64f;
                controller.center = new Vector3(0f, 0.3f, 0f);
                if (speed<=0f)
                {
                    speed = runSpeed;
                    slideSpeed = 0;
                    controller.height = normalHeight;
                    controller.center = new Vector3(0f, 0f, 0f);
                }
                
                wichanim = "Base Layer.empty";
            }
            else
            {
                speed = runSpeed;
                slideSpeed = 0;
                controller.height = normalHeight;
                controller.center = new Vector3(0f, 0f, 0f);
            }
        }
        // if Walking...
        if (isWalking)
        {
            camerak.transform.localRotation = Quaternion.Euler(0, 0, 0);
            timer = MaxTimeOnWall;
            
            wichanim = "Base Layer.empty";
            if (isSliding)
            {
                controller.height = normalHeight;
                controller.center = new Vector3(0f, 0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = !isCrouching;
            }
            
            if (isCrouching)
            {
                controller.height = 2.64f;
                controller.center = new Vector3(0f, 0.3f, 0f);
            } else if (!isCrouching)
            {
                controller.height = normalHeight;
                controller.center = new Vector3(0f, 0f, 0f);
            }

        }
        // if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f || !animator.GetCurrentAnimatorStateInfo(0).IsName("Recoil") ) {
        //     animator.Play(wichanim);
        // }
    }
    // public void climLedge() 
    // {
    //     velocity.y = Mathf.Sqrt(climbHeight * -2 * gravity);
    //     Debug.Log("try to climbwall");
    // }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            godmode = !godmode;
            Debug.Log("y pressed");
        }
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, climableWalls))
        {
            Debug.Log("Did Hit");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        // else
        // {
        //     Debug.Log("......");
        // }
    }

    private void weaponSideAnim(float x)
    {
        if (x<0)
            {
                gun.transform.localRotation = Quaternion.Euler(0, 0, 5);
            }
            else if (x>0) 
            {
                gun.transform.localRotation = Quaternion.Euler(0, 0, -5);
            }
            else if (x==0)
            {
                gun.transform.localRotation = Quaternion.Euler(0, 0, 0);

            }
    }
}
