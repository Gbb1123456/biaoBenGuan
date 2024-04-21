using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;

public class CubeRotAndScale : MonoBehaviour
{

    GameObject go;

    public bool rotate;

    float value;
    private void Update()
    {
        if (Input.GetMouseButtonUp(1) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            go = null;
        }
        if (Input.GetMouseButtonDown(1) && rotate && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000/*, GetComponent<MouseMoveObj>().layerMask*/))
            {
                go = hit.collider.gameObject;
            }
            //Vector3 haxis = Vector3.Cross(fwd, Vector3.up);
            //transform.Rotate(haxis, -Input.GetAxis("Mouse Y") * 7, Space.World);
        }
        if (Input.GetMouseButton(1) && go != null)
        {
            go.transform.Rotate(go.transform.up, -Input.GetAxis("Mouse X") * 7, Space.World);
            //Vector3 fwd = GuZhangManager.Instance.cameraMoveAndRot.transform.forward;
            //fwd.Normalize();
            //Vector3 vaxis = Vector3.Cross(fwd, Vector3.right);
            //go.transform.Rotate(vaxis, -Input.GetAxis("Mouse X") * 7, Space.World);
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            value = Camera.main.fieldOfView - 2;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            value = Camera.main.fieldOfView + 2;
        }
        value = Mathf.Clamp(value, 20, 61);
        Camera.main.fieldOfView = value;
    }
}
