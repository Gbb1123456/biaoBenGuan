using System.Collections;
using UnityEngine;
/**
 * 因为物体在不断随着鼠标的运动而旋转，所以旋转时一定要在世界坐标中，否则我们会看到物体绕着自身的旋转轴旋转，这是不对的。
 * 另外，我们都是通过camera来看到的，所以Rotate的第一个参数axis一定要是camera的某个轴向，左右方向的旋转需要绕着camera的up方向，
 * 上下方向的旋转需要绕着camera的right方向。
 */
public class ModelRotate : MonoBehaviour 
{
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Camera mCamera;
    public void Init(Camera c)
    {
        startPos = transform.localPosition;
        startRot = transform.localRotation;
        mCamera = c;
    }
    private void OnMouseDown()
    {
        if (mCamera == null) return;
        screenPoint = mCamera.WorldToScreenPoint(transform.position);
        offset = transform.position - mCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    private void OnMouseDrag()
    {
        if (mCamera == null) return;
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curWorldPoint = mCamera.ScreenToWorldPoint(curScreenPoint);
        transform.position = curWorldPoint + offset;
    }
    private void Update()
    {
        RotateXY();
    }
    private void RotateXY()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 vaxis = Vector3.Cross(Vector3.forward, Vector3.right);
            transform.Rotate(vaxis, Input.GetAxis("Mouse X") * 7, Space.World);
            Vector3 haxis = Vector3.Cross(Vector3.forward, Vector3.up);
            transform.Rotate(haxis, -Input.GetAxis("Mouse Y") * 7, Space.World);
        }
    }
    public void ResetPosAndRot()
    {
        transform.localPosition = startPos;
        transform.localRotation = startRot;
    }
}
