using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller; 
    public Transform cam; //main cam NOT cinemachine

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    void Update()
    {
        if (BlackMage.casting == false) {
            float horizontal = Input.GetAxisRaw("HorizontalLetters");
            float vertical = Input.GetAxisRaw("VerticalLetters");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            if (direction.magnitude >= 0.1F)
            {
                //smoothing and angle calculating
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; //calculating which direction to point when moving
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
    }
}
