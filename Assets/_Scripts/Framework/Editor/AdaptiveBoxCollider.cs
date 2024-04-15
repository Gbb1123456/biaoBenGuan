

using UnityEditor;
using UnityEngine;

namespace ZXKFrameworkEditor
{
    public class AdaptiveBoxCollider
    {
        [MenuItem("ZXKFramework/Collider/Auto BoxCollider")]
        private static void AutoBoxCollider()
        {
            //如果未选中任何物体 返回
            //GameObject gameObject = Selection.activeGameObject;
            GameObject[] gos = Selection.gameObjects;
            if (gos.Length.Equals(0)) return;
            for (int k = 0; k < gos.Length; k++)
            {
                //计算中心点
                Vector3 center = Vector3.zero;
                var renders = gos[k].GetComponentsInChildren<Renderer>();
                for (int i = 0; i < renders.Length; i++)
                {
                    center += renders[i].bounds.center;
                }
                center /= renders.Length;
                //创建边界盒
                Bounds bounds = new Bounds(center, Vector3.zero);
                foreach (var render in renders)
                {
                    bounds.Encapsulate(render.bounds);
                }
                //先判断当前是否有碰撞器 进行销毁
                var currentCollider = gos[k].GetComponent<Collider>();
                if (currentCollider != null) Object.DestroyImmediate(currentCollider);
                //添加BoxCollider 设置中心点及大小
                var boxCollider = gos[k].AddComponent<BoxCollider>();
                boxCollider.center = bounds.center - gos[k].transform.position;
                boxCollider.size = bounds.size;
            }
        }

        static Vector3 pMax = Vector3.zero;
        static Vector3 pMin = Vector3.zero;
        static Vector3 center = Vector3.zero;


        #region 时而好用

        //[MenuItem("ZXKFramework/Collider/AATest")]
        //private static void AATest()
        //{
        //    GameObject[] gos = Selection.gameObjects;
        //    if (gos.Length.Equals(0)) return;
        //    for (int k = 0; k < gos.Length; k++)
        //    {
        //        Vector3 oldPos = gos[k].transform.position;
        //        Quaternion oldQua = gos[k].transform.rotation;

        //        gos[k].transform.position = Vector3.zero;
        //        gos[k].transform.rotation = Quaternion.identity;

        //        Bounds bounds = ClacBounds(gos[k]);

        //        BoxCollider collider = gos[k].GetComponent<BoxCollider>();
        //        if (collider == null)
        //        {
        //            collider = gos[k].AddComponent<BoxCollider>();
        //        }
        //        collider.center = bounds.center;
        //        collider.size = bounds.size;

        //        gos[k].transform.position = oldPos;
        //        gos[k].transform.rotation = oldQua;
        //    }
        //}

        ///// <summary>
        ///// 计算目标包围盒
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //private static Bounds ClacBounds(GameObject obj)
        //{
        //    Renderer mesh = obj.GetComponent<Renderer>();

        //    if (mesh != null)
        //    {
        //        Bounds b = mesh.bounds;
        //        pMax = b.max;
        //        pMin = b.min;
        //        center = b.center;
        //    }


        //    RecursionClacBounds(obj.transform);

        //    ClacCenter(pMax, pMin, out center);

        //    Vector3 size = new Vector3(pMax.x - pMin.x, pMax.y - pMin.y, pMax.z - pMin.z);
        //    Bounds bound = new Bounds(center, size);
        //    bound.size = size;

        //    bound.extents = size / 2f;

        //    return bound;
        //}
        ///// <summary>
        ///// 计算包围盒中心坐标
        ///// </summary>
        ///// <param name="max"></param>
        ///// <param name="min"></param>
        ///// <param name="center"></param>
        //private static void ClacCenter(Vector3 max, Vector3 min, out Vector3 center)
        //{
        //    float xc = (pMax.x + pMin.x) / 2f;
        //    float yc = (pMax.y + pMin.y) / 2f;
        //    float zc = (pMax.z + pMin.z) / 2f;

        //    center = new Vector3(xc, yc, zc);
        //}
        ///// <summary>
        ///// 计算包围盒顶点
        ///// </summary>
        ///// <param name="obj"></param>
        //private static void RecursionClacBounds(Transform obj)
        //{
        //    if (obj.transform.childCount <= 0)
        //    {
        //        return;
        //    }

        //    foreach (Transform item in obj)
        //    {
        //        Renderer m = item.GetComponent<Renderer>();



        //        if (m != null)
        //        {
        //            Bounds b = m.bounds;
        //            if (pMax.Equals(Vector3.zero) && pMin.Equals(Vector3.zero))
        //            {
        //                pMax = b.max;
        //                pMin = b.min;
        //            }

        //            if (b.max.x > pMax.x)
        //            {
        //                pMax.x = b.max.x;
        //            }

        //            if (b.max.y > pMax.y)
        //            {
        //                pMax.y = b.max.y;
        //            }
        //            if (b.max.z > pMax.z)
        //            {
        //                pMax.z = b.max.z;
        //            }
        //            if (b.min.x < pMin.x)
        //            {
        //                pMin.x = b.min.x;
        //            }

        //            if (b.min.y < pMin.y)
        //            {
        //                pMin.y = b.min.y;
        //            }
        //            if (b.min.z < pMin.z)
        //            {
        //                pMin.z = b.min.z;
        //            }
        //        }
        //        RecursionClacBounds(item);
        //    }
        //}

        #endregion
    }
}