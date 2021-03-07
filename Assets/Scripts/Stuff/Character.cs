using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [Range(1, 20)] public float speed = 1;
    [Range(1, 20)] public float jump = 1;
    [Range(-20, 20)] public float gravity = -9.8f;
    public Animator animator;
    public Weapon weapon;
    public eSpace space = eSpace.World;
    public eMovement movement = eMovement.Free;
    //public Vignette vignette;
    public float turnRate = 3;
    float timer;
    public static int TotalScore { get; set; }


    public enum eSpace 
    {
        World,
        Camera,
        Object
    }

    public enum eMovement
    {
        Free,
        Tank,
        Strafe
    }


    CharacterController characterController;
    bool onGround;
    Vector3 inputDirection;
    Vector3 velocity;
    public Health health;
    Transform cameraTransform;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        health = GetComponent<Health>();

        cameraTransform = Camera.main.transform;
        //StartCoroutine(GameOver());
    }

    void Update()
    {
/*        if (animator.GetBool("Death"))
        {
            if (health.hp <= 0)
            {
                StartCoroutine(GameOver());
            }
            return;
        }*/

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }



        //**
        Quaternion orientation = Quaternion.identity;
        switch (space)
        {
            case eSpace.Camera:
                Vector3 forward = cameraTransform.forward;
                forward.y = 0;
                orientation = Quaternion.LookRotation(forward);
                break;
            case eSpace.Object:
                orientation = transform.rotation;
                break;
            default:
                break;
        }

        Vector3 direction = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        switch (movement)
        {
            case eMovement.Free:
                direction = orientation * inputDirection;
                rotation = (direction.sqrMagnitude > 0) ? Quaternion.LookRotation(direction) : transform.rotation;
                break;
            case eMovement.Tank:
                direction.z = inputDirection.z;
                direction = orientation * direction;

                rotation = orientation * Quaternion.AngleAxis(inputDirection.x, Vector3.up);
                break;
            case eMovement.Strafe:
                direction = orientation * inputDirection;
                rotation = Quaternion.LookRotation(orientation * Vector3.forward);
                break;
            default:
                break;
        }

        //**
        characterController.Move(direction * speed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnRate);
            
            //animator
            animator.SetFloat("Speed", inputDirection.magnitude);
            animator.SetBool("OnGround", characterController.isGrounded);
            animator.SetFloat("VelocityY", velocity.y);

            //Gravity movement
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        GameController.Instance.OnGameOverScreen();
    }


    public void OnDeath()
    {
        animator.SetBool("Death", true);
        EventManager.Instance.TriggerEvent("PlayerDead");
    }

    public void OnFire()
    {
        weapon.Fire(transform.forward);
        //Debug.Log("OnFire");
    }

    public void OnJump()
    {
        if (characterController.isGrounded)
        {
            velocity.y += jump;
        }
    }

    public void OnPunch()
    {
        animator.SetTrigger("Punch");
    }

    public void OnThrow()
    {
        animator.SetTrigger("Throw");
    }

    public void OnMove(InputValue input)
    {
        Vector2 v2 = input.Get<Vector2>();
        inputDirection.x = v2.x;
        inputDirection.z = v2.y;
    }

    public static void AddTotalScore(int points)
    {
        TotalScore += points;
    }

}
