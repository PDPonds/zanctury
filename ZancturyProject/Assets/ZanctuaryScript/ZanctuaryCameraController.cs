using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZanctuaryCameraController : MonoBehaviour
{
    public float movementSpeed;
    public float panBorderThickness;
    public Vector2 panLimit;
    [SerializeField] GameObject UI1;
    [SerializeField] GameObject UI2;
    [SerializeField] GameObject UI3;
    [SerializeField] GameObject UI4;
    [SerializeField] GameObject UI5;
    [SerializeField] GameObject UI6;
    [SerializeField] GameObject UI7;
    [SerializeField] GameObject UI8;
    void Update()
    {
        if (!UI1.activeSelf && !UI2.activeSelf && !UI3.activeSelf &&
           !UI4.activeSelf && !UI5.activeSelf && !UI6.activeSelf &&
           !UI7.activeSelf &&!UI8.activeSelf)
        {
            HandleMovementInput();
        }
    }

    public void HandleMovementInput()
    {
        Vector3 pos = transform.position;

        if(Input.GetKey(KeyCode.W)|| Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= movementSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }

}
