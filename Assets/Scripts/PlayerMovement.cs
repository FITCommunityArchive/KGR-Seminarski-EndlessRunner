using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Rigidbody _playerPhysics;
    private Vector3 moveVector;
#pragma warning disable 0649

    [SerializeField]
    [Range(0.01f,20.0f)]
    private float Speed;

    [SerializeField]
    private float JumpPower;

    [SerializeField]
    private InputManagement inputManagement;

    [SerializeField]
    private GameObject Tiles;

    [SerializeField]
    private GameObject SkateboardObject;
#pragma warning restore 0649
    private float verticalVelocity = 0.0f;
    private float gravity = -20.0f;
    private float Horizontal;
    private float Vertical;
    private float Jump;
    private bool Slow;
    private bool Skateboard;
    private bool Fly;
    private int numOfLeftRotation = 0;
    private int numOfRightRotation = 0;
    private bool isJump=false;
    private bool isDeath = false;
    private bool isFalling = false;
    private bool isSkateboard = false;
    private bool isFlying;
    private Animator animate;
    private GameObject[] colliders;
    bool turn = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManagement.OnAxisInput += OnAxisInputChanged;
        animate = GetComponent<Animator>();
        colliders = GameObject.FindGameObjectsWithTag("Collider");
        SkateboardObject.SetActive(false);
        isFlying=false;
}


void Update()
    {

        if (this.Fly)
        {
            if (((int)PlayerPrefs.GetFloat("Jetpack") > 0) && isSkateboard == false)
            {
                isFlying = true;
                verticalVelocity = 0.0f;
                StartCoroutine(ActivateFlying());
                GetComponent<AssetsManagement>().UseJetpack();

            }
        }


        colliders = GameObject.FindGameObjectsWithTag("Collider");

        moveVector = Vector3.zero;

        if (isFlying==false)
        {
            
        if (!controller.isGrounded && controller.transform.position.y < -1.48f)
        {
            isFalling = true;
            Death();
        }

        else if (controller.isGrounded)
        {
            if (this.Jump > 0)
            {
                isJump = true;
                verticalVelocity = JumpPower;
            }
            else
            {
                isJump = false;
            }
            if (this.Slow)
            {
                
                if ((int)PlayerPrefs.GetFloat("Speed") > 0)
                    {
                        Speed = 7.5f;
                        GetComponent<AssetsManagement>().UseSlowSpeed();
                    }
            }
            if (this.Skateboard)
            {
                    if (((int)PlayerPrefs.GetFloat("Skateboard") > 0) && isFlying == false)
                    {
                        StartCoroutine(ActivateSkateboard());
                        GetComponent<AssetsManagement>().UseSkateboard();
                    }
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;

        }
        }
       
        if (isDeath)
            moveVector = new Vector3(0, 0, 0);
        else if (isFalling)
            moveVector = new Vector3(0, verticalVelocity, 0);
        else if (turn)
        {
            moveVector = new Vector3(Speed, verticalVelocity, this.Vertical * Speed);
        }
        else
            moveVector = new Vector3(this.Horizontal * Speed, verticalVelocity, Speed);


        controller.Move(moveVector * Time.deltaTime);

        if (isJump)
            animate.SetBool("IsJump", true);
        else
            animate.SetBool("IsJump", false);
        if (isDeath)
        {
            animate.SetBool("IsDeath", true);
        }
        else
            animate.SetBool("IsDeath", false);
        if (isFalling)
        {
            animate.SetBool("IsFalling", true);
        }
        else
        {
            animate.SetBool("IsFalling", false);
        }
        if (isSkateboard)
        {
            animate.SetBool("IsSkateboard", true);
        }
        else
        {
            animate.SetBool("IsSkateboard", false);
        }
        if (isFlying)
        {
            animate.SetBool("IsFlying", true);
        }
        else
        {
            animate.SetBool("IsFlying", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.LeftCornerTag))
        {
            ++numOfLeftRotation;
            if (numOfLeftRotation == 1)
            {
            this.transform.Rotate(0.0f, 90.0f, 0.0f);
            turn = true;

            }
        }
        if (collision.gameObject.CompareTag(Constants.RightCornerTag))
        {
            ++numOfRightRotation;

            if (numOfRightRotation == 1)
            {
                this.transform.Rotate(0.0f, -90.0f , 0.0f);
                turn = false;

            }

        }
        if (collision.gameObject.CompareTag(Constants.CounterResetTag))
        {
            numOfLeftRotation = 0;
            numOfRightRotation = 0;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag(Constants.ColliderTag))
        {
            isDeath = true;

            Death();      
        }
        if (hit.gameObject.CompareTag(Constants.CoinTag))
        {
            GetComponent<CollectCoin>().IncreaseCoins();

            Destroy(hit.gameObject);
        }
        if (hit.gameObject.CompareTag(Constants.SpecialCoinTag))
        {
            GetComponent<CollectCoin>().IncreaseSpecialCoin();

            Destroy(hit.gameObject);
        }
        if (hit.gameObject.CompareTag(Constants.LeftWallTag))
        {
           
                this.transform.Rotate(0.0f, 90.0f, 0.0f);
                turn = true;
            Debug.Log(turn);

        }
        if (hit.gameObject.CompareTag(Constants.RightWallTag))
        {

        
                this.transform.Rotate(0.0f, -90.0f, 0.0f);
                turn = false;
            Debug.Log(turn);

        }
    }
    private void OnAxisInputChanged(float Horizontal,float Vertical, float Jump, bool Slow, bool Space, bool Shift)
    {
        this.Horizontal = Horizontal;
        this.Vertical = Vertical;
        this.Jump = Jump;
        this.Slow = Slow;
        this.Skateboard = Space;
        this.Fly = Shift;
    }
    public void SetSpeed(float modifier)
    {
        Speed += modifier;
    }

    private void Death()
    {
        //isDeath = true;
        SkateboardObject.SetActive(false);
        GetComponent<CollectCoin>().OnDeath();
        GetComponent<Score>().OnDeath();
    }
    IEnumerator ActivateSkateboard()
    {
        foreach (GameObject col in colliders)
        {
            col.GetComponent<Collider>().enabled = false;
        }
        isSkateboard = true;
        SkateboardObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        foreach (GameObject col in colliders)
        {
            col.GetComponent<Collider>().enabled = true;
        }
        isSkateboard = false;
        SkateboardObject.SetActive(false);
    }
    IEnumerator ActivateFlying()
    {
        isFlying = true;

        GetComponent<CapsuleCollider>().radius = 1f;
        foreach (GameObject col in colliders)
        {
            col.GetComponent<Collider>().enabled = false;
        }
        yield return new WaitForSeconds(10f);
        isFlying = false;
        GetComponent<CapsuleCollider>().radius = 0.4f;

        foreach (GameObject col in colliders)
        {
            col.GetComponent<Collider>().enabled = true;
        }
    }
}
