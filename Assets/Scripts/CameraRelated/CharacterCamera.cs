using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CharacterCamera : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset;
    [Range(0, 20)]public float rate = 1;
    public bool orientToTarget = true;
    public bool clampYaw = true;
    public int cameramaxDistance = 5;

    Vector2 inputRotation = Vector2.zero;
    float pitch = 30;
    float yaw;
    float distance = 2;


    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    void FixedUpdate()
    {
        Quaternion rotationBase = (orientToTarget) ? targetTransform.rotation : Quaternion.identity;
        Quaternion rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.up);

        Vector3 targetPosition = targetTransform.position + (rotation * Vector3.back * distance);
        //Vector3 targetPosition = targetTransform.position + (targetTransform.rotation * offset);

        Ray ray = new Ray(targetTransform.position, (targetPosition - targetTransform.position));
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            targetPosition = raycastHit.point;
            Vector3 v = targetPosition - targetTransform.position;
            targetPosition = targetTransform.position + Vector3.ClampMagnitude(v, cameramaxDistance);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, rate * Time.deltaTime);

        Vector3 direction = targetTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void OnPitch(InputAction.CallbackContext callbackContext)
    {
        pitch += callbackContext.ReadValue<float>();
        pitch = Mathf.Clamp(pitch, 0, 20);
    }

    public void OnYaw(InputAction.CallbackContext callbackContext)
    {
        yaw += callbackContext.ReadValue<float>();
        if(clampYaw) yaw = Mathf.Clamp(yaw, -70, 70);
    }

    public void OnDistance(InputAction.CallbackContext callbackContext)
    {
        distance += callbackContext.ReadValue<float>();
        distance = Mathf.Clamp(distance, 3, 12);
    }

    public void OnCenter(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started)
        {
            yaw = 0;
        }
    }
}
