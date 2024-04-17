using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System;

namespace SK.Framework
{
    public static class TransformExtension
    {
        /// <summary>
        /// 访问transform的每个子物体（不包括孙子物体）
        /// </summary>
        /// <param name="transform">指定的transform</param>
        /// <param name="onGet">访问时的委托事件</param>
        public static void Foreach(this Transform transform, Action<Transform> onGet)
        {
            foreach (Transform child in transform)
            {
                if (onGet != null)
                    onGet(child);
            }
        }

        /// <summary>
        /// 深度遍历子物体
        /// </summary>
        /// <param name="transform">指定的transform</param>
        /// <param name="onGet">访问时的委托事件</param>
        public static void TraverseChildrenInDepth(this Transform transform, Action<Transform> onGet)
        {
            foreach (Transform child in transform)
            {
                onGet(child);

                if (child.childCount != 0)
                {
                    TraverseChildrenInDepth(child, onGet);
                }
            }
        }

        /// <summary>
        /// 控制指定名字的子物体（非孙子物体）的显隐
        /// </summary>
        /// <param name="transform">父物体</param>
        /// <param name="childrenName">子物体名字</param>
        /// <param name="state">显隐状态</param>
        /// <returns>返回指定名字的子物体</returns>
        public static Transform[] ToggleChildren(this Transform transform, string childrenName, bool state)
        {
            return transform.ToggleChildren(child => child.name == childrenName, state);
        }

        /// <summary>
        /// 控制指定层级顺序的子物体（非孙子物体）的显隐
        /// </summary>
        /// <param name="transform">父物体</param>
        /// <param name="index">层级顺序</param>
        /// <param name="state">显隐状态</param>
        /// <returns>返回指定层级顺序的子物体</returns>
        public static Transform[] ToggleChildren(this Transform transform, int index, bool state)
        {
            return transform.ToggleChildren(child => child.GetSiblingIndex() == index, state);
        }

        /// <summary>
        /// 控制指定条件的子物体（非孙子物体）的显隐
        /// </summary>
        /// <param name="transform">父物体</param>
        /// <param name="condition">选择的子物体的条件</param>
        /// <param name="state">显隐状态</param>
        /// <returns>返回指定条件的子物体</returns>
        public static Transform[] ToggleChildren(this Transform transform, Func<Transform, bool> condition, bool state)
        {
            var children = new List<Transform>();

            foreach (Transform child in transform)
            {
                var flag = condition(child);
                child.gameObject.SetActive(state);
                children.Add(child);
            }

            return children.ToArray();
        }

        /// <summary>
        /// 深度遍历子物体，控制指定名字的子物体的显隐
        /// </summary>
        /// <param name="transform">父物体</param>
        /// <param name="childrenName">指定子物体的名字</param>
        /// <param name="state">显隐状态</param>
        /// <returns>返回控制显隐的子物体</returns>
        public static Transform[] ToggleChildrenInDepth(this Transform transform, string childrenName, bool state)
        {
            return transform.ToggleChildrenInDepth(child => child.name == childrenName, state);
        }

        /// <summary>
        /// 深度遍历子物体，控制指定条件的子物体的显隐
        /// </summary>
        /// <param name="transform">父物体</param>
        /// <param name="condition">指定子物体的条件</param>
        /// <param name="state">显隐状态</param>
        /// <returns>返回控制显隐的子物体</returns>
        public static Transform[] ToggleChildrenInDepth(this Transform transform, Func<Transform, bool> condition, bool state)
        {
            var children = new List<Transform>();

            TraverseChildren(transform);

            return children.ToArray();

            void TraverseChildren(Transform parent)
            {
                foreach (Transform child in parent)
                {
                    if (condition(child))
                    {
                        child.gameObject.SetActive(state);
                        children.Add(child);
                    }

                    if (child.childCount != 0)
                    {
                        TraverseChildren(child);
                    }
                }
            }
        }

        /// <summary>
        /// 只显示指定名字的子物体（非孙子物体），其他隐藏
        /// </summary>
        /// <param name="transform">父物体</param>
        /// <param name="childName">指定子物体的名字</param>
        /// <returns>显示的子物体</returns>
        public static Transform[] ActiveChildren(this Transform transform, string childName)
        {
            return ActiveChildren(transform, child => child.name == childName);
        }

        /// <summary>
        /// 只显示指定层级顺序的子物体（非孙子物体），其他隐藏
        /// </summary>
        /// <param name="transform">父物体</param>
        /// <param name="index">指定子物体的层级顺序</param>
        /// <returns>显示的子物体</returns>
        public static Transform[] ActiveChildren(this Transform transform, int index)
        {
            return transform.ActiveChildren(child => child.GetSiblingIndex() == index);
        }

        /// <summary>
        /// 只显示指定条件的子物体（非孙子物体），其他隐藏
        /// </summary>
        /// <param name="transform">父物体</param>
        /// <param name="condition">指定物体的条件</param>
        /// <returns>显示的子物体</returns>
        public static Transform[] ActiveChildren(this Transform transform, Func<Transform, bool> condition)
        {
            List<Transform> activeChildren = new List<Transform>();

            foreach (Transform child in transform)
            {
                bool flag = condition(child);
                child.gameObject.SetActive(flag);

                if (flag)
                    activeChildren.Add(child);
            }

            return activeChildren.ToArray();
        }

        /// <summary>
        /// 深度遍历子物体，只显示指定名字的物体
        /// </summary>
        /// <param name="transform">父物体</param>
        /// <param name="childName">子物体名字</param>
        /// <returns>显示的子物体</returns>
        public static void ActiveChildrenInDepth(this Transform transform, string childName)
        {
            ActiveChildrenInDepth(transform, child => child.name == childName);
        }

        /// <summary>
        /// 深度遍历子物体，只显示满足条件的物体
        /// </summary>
        /// <param name="transform">父物体</param>
        /// <param name="condition">子物体应满足的条件</param>
        /// <returns>显示的子物体</returns>
        public static Transform[] ActiveChildrenInDepth(this Transform transform, Func<Transform, bool> condition)
        {
            var children = new List<Transform>();

            Traverse(transform);

            return children.ToArray();

            void Traverse(Transform parent)
            {
                foreach (Transform child in parent)
                {
                    var flag = condition(child);
                    if (flag)
                    {
                        children.Add(child);
                    }
                    child.gameObject.SetActive(flag);

                    if (child.childCount != 0)
                    {
                        Traverse(child);
                    }
                }
            }
        }

        /// <summary>
        /// 删除所有子物体
        /// </summary>
        /// <param name="transform">指定的父物体</param>
        public static void RemoveAllChildren(this Transform transform, bool immediate)
        {
            transform.RemoveChildren(_ => true, immediate);
        }

        /// <summary>
        /// 删除指定子物体（仅父物体下同层的子物体）
        /// </summary>
        /// <param name="transform">指定的父物体</param>
        /// <param name="condition">满足的条件</param>
        public static void RemoveChildren(this Transform transform, Func<Transform, bool> condition, bool immediate)
        {
            if (immediate)
            {
                for (int i = 0; i < transform.childCount;)
                {
                    var child = transform.GetChild(i);
                    if (condition(child))
                    {
                        if (immediate)
                        {
                            UnityEngine.Object.DestroyImmediate(child.gameObject);
                        }
                        else
                        {
                            UnityEngine.Object.Destroy(child.gameObject);
                            i++;
                        }
                    }
                    else if (immediate)
                    {
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// 获得组件，若没有则添加
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="transform">指定的transform</param>
        /// <returns>组件</returns>
        public static T GetOrAddComponent<T>(this Transform transform) where T : Component
        {
            return transform.gameObject.GetOrAddComponent<T>();
        }

        /// <summary>
        /// 获取父物体的Transform
        /// </summary>
        /// <param name="transform">指定的transform</param>
        /// <param name="name">父物体的名字</param>
        /// <returns>父物体的Transform</returns>
        public static Transform GetParent(this Transform transform, string name)
        {
            GameObject go = transform.gameObject.GetParent(name);
            return go == null ? null : go.transform;
        }

        /// <summary>
        /// 根据索引获取兄弟节点
        /// </summary>
        /// <param name="transform">指定的transform</param>
        /// <param name="index">在层级中的索引</param>
        /// <returns>兄弟节点</returns>
        public static Transform GetBrother(this Transform transform, int index)
        {
            GameObject go = transform.gameObject.GetBrother(index);
            return go == null ? null : go.transform;
        }

        /// <summary>
        /// 根据名字获取兄弟节点
        /// </summary>
        /// <param name="transform">指定的transform</param>
        /// <param name="name">兄弟节点的名字</param>
        /// <returns>兄弟节点</returns>
        public static Transform GetBrother(this Transform transform, string name)
        {
            GameObject go = transform.gameObject.GetBrother(name);
            return go == null ? null : go.transform;
        }

        /// <summary>
        /// 获得子物体的Transform
        /// </summary>
        /// <param name="transform">指定的transform</param>
        /// <param name="name">子物体的名字</param>
        /// <returns>子物体的Transform</returns>
        public static Transform GetChild(this Transform transform, string name)
        {
            GameObject go = transform.gameObject.GetChild(name);
            return go == null ? null : go.transform;
        }

        public static Transform[] GetChildren(this Transform transform, Func<Transform, bool> condition)
        {
            var children = new List<Transform>();
            foreach (Transform child in transform)
            {
                if (condition(child))
                    children.Add(child);
            }
            return children.ToArray();
        }

        public static Transform[] GetChildrenInDepth(this Transform transform, Func<Transform, bool> condition)
        {
            var children = new List<Transform>();

            Traverse(transform, false);

            return children.ToArray();

            void Traverse(Transform parent, bool add)
            {
                if (add && condition(parent))
                {
                    children.Add(parent);
                }
                foreach (Transform child in parent)
                {
                    Traverse(child, true);
                }
            }
        }
        public static T SetSiblingIndex<T>(this T self, int index) where T : Component
        {
            self.transform.SetSiblingIndex(index);
            return self;
        }
        public static T SetAsFirstSibling<T>(this T self) where T : Component
        {
            self.transform.SetAsFirstSibling();
            return self;
        }
        public static T SetAsLastSibling<T>(this T self) where T : Component
        {
            self.transform.SetAsLastSibling();
            return self;
        }
        public static T GetSiblingIndex<T>(this T self, out int index) where T : Component
        {
            index = self.transform.GetSiblingIndex();
            return self;
        }
        public static T GetPosition<T>(this T self, out Vector3 position) where T : Component
        {
            position = self.transform.position;
            return self;
        }
        public static T SetPosition<T>(this T self, Vector3 pos) where T : Component
        {
            self.transform.position = pos;
            return self;
        }
        public static T SetPosition<T>(this T self, float x, float y, float z) where T : Component
        {
            Vector3 pos = self.transform.position;
            pos.x = x;
            pos.y = y;
            pos.z = z;
            self.transform.position = pos;
            return self;
        }
        public static T SetPositionX<T>(this T self, float x) where T : Component
        {
            Vector3 pos = self.transform.position;
            pos.x = x;
            self.transform.position = pos;
            return self;
        }
        public static T SetPositionY<T>(this T self, float y) where T : Component
        {
            Vector3 pos = self.transform.position;
            pos.y = y;
            self.transform.position = pos;
            return self;
        }
        public static T SetPositionZ<T>(this T self, float z) where T : Component
        {
            Vector3 pos = self.transform.position;
            pos.z = z;
            self.transform.position = pos;
            return self;
        }
        public static T PositionIdentity<T>(this T self) where T : Component
        {
            self.transform.position = Vector3.zero;
            return self;
        }
        public static T RotationIdentity<T>(this T self) where T : Component
        {
            self.transform.rotation = Quaternion.identity;
            return self;
        }
        public static T SetEulerAngles<T>(this T self, Vector3 eulerAngles) where T : Component
        {
            self.transform.eulerAngles = eulerAngles;
            return self;
        }
        public static T SetEulerAngles<T>(this T self, float x, float y, float z) where T : Component
        {
            Vector3 eulerAngles = self.transform.eulerAngles;
            eulerAngles.x = x;
            eulerAngles.y = y;
            eulerAngles.z = z;
            self.transform.eulerAngles = eulerAngles;
            return self;
        }
        public static T SetEulerAnglesX<T>(this T self, float x) where T : Component
        {
            Vector3 eulerAngles = self.transform.eulerAngles;
            eulerAngles.x = x;
            self.transform.eulerAngles = eulerAngles;
            return self;
        }
        public static T SetEulerAnglesY<T>(this T self, float y) where T : Component
        {
            Vector3 eulerAngles = self.transform.eulerAngles;
            eulerAngles.y = y;
            self.transform.eulerAngles = eulerAngles;
            return self;
        }
        public static T SetEulerAnglesZ<T>(this T self, float z) where T : Component
        {
            Vector3 eulerAngles = self.transform.eulerAngles;
            eulerAngles.z = z;
            self.transform.eulerAngles = eulerAngles;
            return self;
        }
        public static T EulerAnglesIdentity<T>(this T self) where T : Component
        {
            self.transform.eulerAngles = Vector3.zero;
            return self;
        }
        public static T SetLocalPosition<T>(this T self, Vector3 localPos) where T : Component
        {
            self.transform.localPosition = localPos;
            return self;
        }
        public static T SetLocalPosition<T>(this T self, float x, float y, float z) where T : Component
        {
            Vector3 localPos = self.transform.localPosition;
            localPos.x = x;
            localPos.y = y;
            localPos.z = z;
            self.transform.localPosition = localPos;
            return self;
        }
        public static T SetLocalPositionX<T>(this T self, float x) where T : Component
        {
            Vector3 localPos = self.transform.localPosition;
            localPos.x = x;
            self.transform.localPosition = localPos;
            return self;
        }
        public static T SetLocalPositionY<T>(this T self, float y) where T : Component
        {
            Vector3 localPos = self.transform.localPosition;
            localPos.y = y;
            self.transform.localPosition = localPos;
            return self;
        }
        public static T SetLocalPositionZ<T>(this T self, float z) where T : Component
        {
            Vector3 localPos = self.transform.localPosition;
            localPos.z = z;
            self.transform.localPosition = localPos;
            return self;
        }
        public static T LocalPositionIdentity<T>(this T self) where T : Component
        {
            self.transform.localPosition = Vector3.zero;
            return self;
        }
        public static T LocalRotationIdentity<T>(this T self) where T : Component
        {
            self.transform.localRotation = Quaternion.identity;
            return self;
        }
        public static T SetLocalEulerAngles<T>(this T self, Vector3 localEulerAngles) where T : Component
        {
            self.transform.localEulerAngles = localEulerAngles;
            return self;
        }
        public static T SetLocalEulerAngles<T>(this T self, float x, float y, float z) where T : Component
        {
            Vector3 localEulerAngles = self.transform.localEulerAngles;
            localEulerAngles.x = x;
            localEulerAngles.y = y;
            localEulerAngles.z = z;
            self.transform.localEulerAngles = localEulerAngles;
            return self;
        }
        public static T SetLocalEulerAnglesX<T>(this T self, float x) where T : Component
        {
            Vector3 localEulerAngles = self.transform.localEulerAngles;
            localEulerAngles.x = x;
            self.transform.localEulerAngles = localEulerAngles;
            return self;
        }
        public static T SetLocalEulerAnglesY<T>(this T self, float y) where T : Component
        {
            Vector3 localEulerAngles = self.transform.localEulerAngles;
            localEulerAngles.y = y;
            self.transform.localEulerAngles = localEulerAngles;
            return self;
        }
        public static T SetLocalEulerAnglesZ<T>(this T self, float z) where T : Component
        {
            Vector3 localEulerAngles = self.transform.localEulerAngles;
            localEulerAngles.z = z;
            self.transform.localEulerAngles = localEulerAngles;
            return self;
        }
        public static T LocalEulerAnglesIdentity<T>(this T self) where T : Component
        {
            self.transform.localEulerAngles = Vector3.zero;
            return self;
        }
        public static T SetLocalScale<T>(this T self, Vector3 localScale) where T : Component
        {
            self.transform.localScale = localScale;
            return self;
        }
        public static T SetLocalScale<T>(this T self, float x, float y, float z) where T : Component
        {
            Vector3 localScale = self.transform.localScale;
            localScale.x = x;
            localScale.y = y;
            localScale.z = z;
            self.transform.localScale = localScale;
            return self;
        }
        public static T SetLocalScaleX<T>(this T self, float x) where T : Component
        {
            Vector3 localScale = self.transform.localScale;
            localScale.x = x;
            self.transform.localScale = localScale;
            return self;
        }
        public static T SetLocalScaleY<T>(this T self, float y) where T : Component
        {
            Vector3 localScale = self.transform.localScale;
            localScale.y = y;
            self.transform.localScale = localScale;
            return self;
        }
        public static T SetLocalScaleZ<T>(this T self, float z) where T : Component
        {
            Vector3 localScale = self.transform.localScale;
            localScale.z = z;
            self.transform.localScale = localScale;
            return self;
        }
        public static T LocalScaleIdentity<T>(this T self) where T : Component
        {
            self.transform.localScale = Vector3.one;
            return self;
        }
        public static T Identity<T>(this T self) where T : Component
        {
            self.transform.position = Vector3.zero;
            self.transform.rotation = Quaternion.Euler(Vector3.zero);
            self.transform.localScale = Vector3.one;
            return self;
        }
        public static T LocalIdentity<T>(this T self) where T : Component
        {
            self.transform.localPosition = Vector3.zero;
            self.transform.localRotation = Quaternion.Euler(Vector3.zero);
            self.transform.localScale = Vector3.one;
            return self;
        }
        public static T SetParent<T>(this T self, Component parent, bool worldPositionStays = true) where T : Component
        {
            self.transform.SetParent(parent.transform, worldPositionStays);
            return self;
        }
        public static T SetAsRootTransform<T>(this T self) where T : Component
        {
            self.transform.SetParent(null);
            return self;
        }
        public static T DetachChildren<T>(this T self) where T : Component
        {
            self.transform.DetachChildren();
            return self;
        }
        public static T LookAt<T>(this T self, Vector3 worldPosition) where T : Component
        {
            self.transform.LookAt(worldPosition);
            return self;
        }
        public static T LookAt<T>(this T self, Vector3 worldPosition, Vector3 worldUp) where T : Component
        {
            self.transform.LookAt(worldPosition, worldUp);
            return self;
        }
        public static T LookAt<T>(this T self, Transform target) where T : Component
        {
            self.transform.LookAt(target);
            return self;
        }
        public static T LookAt<T>(this T self, Transform target, Vector3 worldUp) where T : Component
        {
            self.transform.LookAt(target, worldUp);
            return self;
        }
        public static T Rotate<T>(this T self, Vector3 eulers) where T : Component
        {
            self.transform.Rotate(eulers);
            return self;
        }
        public static T Rotate<T>(this T self, Vector3 eulers, Space relativeTo) where T : Component
        {
            self.transform.Rotate(eulers, relativeTo);
            return self;
        }
        public static T Rotate<T>(this T self, Vector3 axis, float angle) where T : Component
        {
            self.transform.Rotate(axis, angle);
            return self;
        }
        public static T Rotate<T>(this T self, Vector3 axis, float angle, Space relativeTo) where T : Component
        {
            self.transform.Rotate(axis, angle, relativeTo);
            return self;
        }
        public static T Rotate<T>(this T self, float xAngle, float yAngle, float zAngle) where T : Component
        {
            self.transform.Rotate(xAngle, yAngle, zAngle);
            return self;
        }
        public static T Rotate<T>(this T self, float xAngle, float yAngle, float zAngle, Space relativeTo) where T : Component
        {
            self.transform.Rotate(xAngle, yAngle, zAngle, relativeTo);
            return self;
        }
        public static T CopyTransformValues<T>(this T self, Component target) where T : Component
        {
            self.transform.position = target.transform.position;
            self.transform.rotation = target.transform.rotation;
            self.transform.localScale = target.transform.localScale;
            return self;
        }
        public static T GetFullName<T>(this T self, out string fullName) where T : Component
        {
            List<Transform> tfs = new List<Transform>();
            Transform tf = self.transform;
            tfs.Add(tf);
            while (tf.parent)
            {
                tf = tf.parent;
                tfs.Add(tf);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(tfs[tfs.Count - 1].name);
            for (int i = tfs.Count - 2; i >= 0; i--)
            {
                sb.Append("/" + tfs[i].name);
            }
            fullName = sb.ToString();
            return self;
        }
        public static T GetComponentOnChild<T>(this Transform self, int childIndex) where T : Component
        {
            if (childIndex > self.childCount - 1) return null;
            return self.GetChild(childIndex).GetComponent<T>();
        }
    }
}