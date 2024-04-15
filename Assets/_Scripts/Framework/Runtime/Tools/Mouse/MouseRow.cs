
using UnityEngine;
//模型控制物体旋转
public class MouseRow : MonoBehaviour
{
    private Rigidbody rigidbod;
    public bool isUse = true;
    public float speed = 2.5f;//旋转跟随速度
    void Start()
    {
        rigidbod = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!isUse) return;
        if (Input.GetMouseButton(0))
        {
            float OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
            float OffsetY = Input.GetAxis("Mouse Y");//获取鼠标y轴的偏移量
            transform.Rotate(new Vector3(OffsetY, -OffsetX, 0) * speed, Space.World);
        }
    }
}
