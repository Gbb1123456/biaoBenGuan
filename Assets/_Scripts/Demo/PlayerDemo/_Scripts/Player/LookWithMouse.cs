using UnityEngine;
using UnityEngine.EventSystems;

public class LookWithMouse : MonoBehaviour
{
    const float k_MouseSensitivityMultiplier = 0.01f;

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    public bool MouseButton1CtrlCameraMove = false;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * k_MouseSensitivityMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * k_MouseSensitivityMultiplier;

        if (MouseButton1CtrlCameraMove && Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            Move(mouseY, mouseX);
        }
        if (!MouseButton1CtrlCameraMove && Input.GetMouseButton(0))
        {
            Move(mouseY, mouseX);
        }
    }

    private void Move(float Y, float X)
    {
        xRotation -= Y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * X);
    }
}
