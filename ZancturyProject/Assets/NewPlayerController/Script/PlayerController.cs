using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float walkSpeed = 10f;
    public float runSpeed = 15f;

    private Vector2 move, mouseLook;
    private Vector3 rotationTarget;

    public bool isCrouch;
    public bool isAim;
    public bool isRun;

    public bool canMove;
    Animator anim;
    CombatSystem _combo;
    [SerializeField] private float anim_ForwardAmout;
    [SerializeField] private float anim_TurnAmout;
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }
    private void Start()
    {
        isAim = false;
        anim = GetComponent<Animator>();
        isRun = false;
        _combo = GetComponent<CombatSystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isAim)
            {
                isAim = false;
            }
            else
            {
                isAim = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRun = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            isCrouch = true;
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            isCrouch = false;
        }

        if (isCrouch)
        {
            anim.SetBool("Crouch", true);
            isRun = false;
        }
        else
        {
            anim.SetBool("Crouch", false);
        }

        if(canMove)
        {
            if (isAim)
            {
                isRun = false;
                _combo.canAtk = false;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(mouseLook);

                if (Physics.Raycast(ray, out hit))
                {
                    rotationTarget = hit.point;
                }
                movePlayerWithAim();
                anim.SetBool("Aim", true); 
            }
            else
            {
                _combo.canAtk = true;
                movePlayer();
                anim.SetBool("Aim", false);
            }
        }
        
        if (isRun)
        {
            anim.SetBool("Run", true);
            speed += 2 * Time.deltaTime;
            if (speed >= runSpeed)
            {
                speed = runSpeed;
            }
        }
        else
        {
            anim.SetBool("Run", false);
            speed -= 3 * Time.deltaTime;
            if (speed <= walkSpeed)
            {
                speed = walkSpeed;
            }
        }
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (movement.magnitude > 1f) move.Normalize();

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 300 * Time.deltaTime);

            var rotatedVector = transform.rotation * Vector3.forward;
        }
        anim_ForwardAmout = movement.normalized.z;
        anim_TurnAmout = Mathf.Atan2(movement.normalized.x, movement.normalized.z);

        anim.SetFloat("Forward", anim_ForwardAmout, 0.1f, Time.deltaTime);
        anim.SetFloat("Turn", anim_TurnAmout, 0.1f, Time.deltaTime);
    }

    public void movePlayerWithAim()
    {
        var lookPos = rotationTarget - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);

        Vector3 aimDiraction = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

        if (aimDiraction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.5f);

        }
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        anim.SetFloat("Forward", movement.normalized.z, 0.1f, Time.deltaTime);
        anim.SetFloat("Turn", movement.normalized.x, 0.1f, Time.deltaTime);
    }

}
