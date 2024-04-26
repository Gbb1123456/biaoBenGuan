using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;

public class CubeRotAndScale : MonoBehaviour
{

    GameObject go;

    public bool rotate;

    float value;

    GameModel gameModel;
    float modelScaleX;
    float modelScaleY;
    float modelScaleZ;

    Camera ca;

    public bool isWorldAxis;

    private void Start()
    {
        gameModel = MVC.GetModel<GameModel>();
        modelScaleX = 1;
        modelScaleY = 1;
        modelScaleZ = 1;
        ca = GameManager.Instance.transform.FindFirst("PlayerControllerFPS").FindFirst<Camera>("Camera");
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            go = null;
        }
        if (Input.GetMouseButtonDown(1) && rotate && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = ca.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000/*, GetComponent<MouseMoveObj>().layerMask*/))
            {
                go = hit.collider.gameObject;
                Debug.LogError("µã»÷µ½ÁË£º" + go.name);
            }
            //Vector3 haxis = Vector3.Cross(fwd, Vector3.up);
            //transform.Rotate(haxis, -Input.GetAxis("Mouse Y") * 7, Space.World);
        }
        if (Input.GetMouseButton(1) && go != null)
        {
            if (isWorldAxis)
            {
                go.transform.Rotate(Vector3.up, -Input.GetAxis("Mouse X") * 7, Space.World);
            }
            else
            {
                go.transform.Rotate(go.transform.up, -Input.GetAxis("Mouse X") * 7, Space.World);
            }
            //Vector3 fwd = GuZhangManager.Instance.cameraMoveAndRot.transform.forward;
            //fwd.Normalize();
            //Vector3 vaxis = Vector3.Cross(fwd, Vector3.right);
            //go.transform.Rotate(vaxis, -Input.GetAxis("Mouse X") * 7, Space.World);
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0 && gameModel.lookModel != null)
        {
            //value = Camera.main.fieldOfView - 2;
            modelScaleX = gameModel.lookModel.transform.localScale.x + .1f;
            modelScaleY = gameModel.lookModel.transform.localScale.y + .1f;
            modelScaleZ = gameModel.lookModel.transform.localScale.z + .1f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            modelScaleX = gameModel.lookModel.transform.localScale.x - .1f;
            modelScaleY = gameModel.lookModel.transform.localScale.y - .1f;
            modelScaleZ = gameModel.lookModel.transform.localScale.z - .1f;
        }
        //value = Mathf.Clamp(value, 20, 61);
        modelScaleX = Mathf.Clamp(modelScaleX, 1.0f, 1.5f);
        modelScaleY = Mathf.Clamp(modelScaleY, 1.0f, 1.5f);
        modelScaleZ = Mathf.Clamp(modelScaleZ, 1.0f, 1.5f);
        gameModel.lookModel.transform.localScale = new Vector3(modelScaleX, modelScaleY, modelScaleZ);
        //Camera.main.fieldOfView = value;
    }
}
