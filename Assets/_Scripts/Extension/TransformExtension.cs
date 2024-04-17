using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System;

namespace SK.Framework
{
    public static class TransformExtension
    {
        /// <summary>
        /// ����transform��ÿ�������壨�������������壩
        /// </summary>
        /// <param name="transform">ָ����transform</param>
        /// <param name="onGet">����ʱ��ί���¼�</param>
        public static void Foreach(this Transform transform, Action<Transform> onGet)
        {
            foreach (Transform child in transform)
            {
                if (onGet != null)
                    onGet(child);
            }
        }

        /// <summary>
        /// ��ȱ���������
        /// </summary>
        /// <param name="transform">ָ����transform</param>
        /// <param name="onGet">����ʱ��ί���¼�</param>
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
        /// ����ָ�����ֵ������壨���������壩������
        /// </summary>
        /// <param name="transform">������</param>
        /// <param name="childrenName">����������</param>
        /// <param name="state">����״̬</param>
        /// <returns>����ָ�����ֵ�������</returns>
        public static Transform[] ToggleChildren(this Transform transform, string childrenName, bool state)
        {
            return transform.ToggleChildren(child => child.name == childrenName, state);
        }

        /// <summary>
        /// ����ָ���㼶˳��������壨���������壩������
        /// </summary>
        /// <param name="transform">������</param>
        /// <param name="index">�㼶˳��</param>
        /// <param name="state">����״̬</param>
        /// <returns>����ָ���㼶˳���������</returns>
        public static Transform[] ToggleChildren(this Transform transform, int index, bool state)
        {
            return transform.ToggleChildren(child => child.GetSiblingIndex() == index, state);
        }

        /// <summary>
        /// ����ָ�������������壨���������壩������
        /// </summary>
        /// <param name="transform">������</param>
        /// <param name="condition">ѡ��������������</param>
        /// <param name="state">����״̬</param>
        /// <returns>����ָ��������������</returns>
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
        /// ��ȱ��������壬����ָ�����ֵ������������
        /// </summary>
        /// <param name="transform">������</param>
        /// <param name="childrenName">ָ�������������</param>
        /// <param name="state">����״̬</param>
        /// <returns>���ؿ���������������</returns>
        public static Transform[] ToggleChildrenInDepth(this Transform transform, string childrenName, bool state)
        {
            return transform.ToggleChildrenInDepth(child => child.name == childrenName, state);
        }

        /// <summary>
        /// ��ȱ��������壬����ָ�������������������
        /// </summary>
        /// <param name="transform">������</param>
        /// <param name="condition">ָ�������������</param>
        /// <param name="state">����״̬</param>
        /// <returns>���ؿ���������������</returns>
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
        /// ֻ��ʾָ�����ֵ������壨���������壩����������
        /// </summary>
        /// <param name="transform">������</param>
        /// <param name="childName">ָ�������������</param>
        /// <returns>��ʾ��������</returns>
        public static Transform[] ActiveChildren(this Transform transform, string childName)
        {
            return ActiveChildren(transform, child => child.name == childName);
        }

        /// <summary>
        /// ֻ��ʾָ���㼶˳��������壨���������壩����������
        /// </summary>
        /// <param name="transform">������</param>
        /// <param name="index">ָ��������Ĳ㼶˳��</param>
        /// <returns>��ʾ��������</returns>
        public static Transform[] ActiveChildren(this Transform transform, int index)
        {
            return transform.ActiveChildren(child => child.GetSiblingIndex() == index);
        }

        /// <summary>
        /// ֻ��ʾָ�������������壨���������壩����������
        /// </summary>
        /// <param name="transform">������</param>
        /// <param name="condition">ָ�����������</param>
        /// <returns>��ʾ��������</returns>
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
        /// ��ȱ��������壬ֻ��ʾָ�����ֵ�����
        /// </summary>
        /// <param name="transform">������</param>
        /// <param name="childName">����������</param>
        /// <returns>��ʾ��������</returns>
        public static void ActiveChildrenInDepth(this Transform transform, string childName)
        {
            ActiveChildrenInDepth(transform, child => child.name == childName);
        }

        /// <summary>
        /// ��ȱ��������壬ֻ��ʾ��������������
        /// </summary>
        /// <param name="transform">������</param>
        /// <param name="condition">������Ӧ���������</param>
        /// <returns>��ʾ��������</returns>
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
        /// ɾ������������
        /// </summary>
        /// <param name="transform">ָ���ĸ�����</param>
        public static void RemoveAllChildren(this Transform transform, bool immediate)
        {
            transform.RemoveChildren(_ => true, immediate);
        }

        /// <summary>
        /// ɾ��ָ�������壨����������ͬ��������壩
        /// </summary>
        /// <param name="transform">ָ���ĸ�����</param>
        /// <param name="condition">���������</param>
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
        /// ����������û�������
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="transform">ָ����transform</param>
        /// <returns>���</returns>
        public static T GetOrAddComponent<T>(this Transform transform) where T : Component
        {
            return transform.gameObject.GetOrAddComponent<T>();
        }

        /// <summary>
        /// ��ȡ�������Transform
        /// </summary>
        /// <param name="transform">ָ����transform</param>
        /// <param name="name">�����������</param>
        /// <returns>�������Transform</returns>
        public static Transform GetParent(this Transform transform, string name)
        {
            GameObject go = transform.gameObject.GetParent(name);
            return go == null ? null : go.transform;
        }

        /// <summary>
        /// ����������ȡ�ֵܽڵ�
        /// </summary>
        /// <param name="transform">ָ����transform</param>
        /// <param name="index">�ڲ㼶�е�����</param>
        /// <returns>�ֵܽڵ�</returns>
        public static Transform GetBrother(this Transform transform, int index)
        {
            GameObject go = transform.gameObject.GetBrother(index);
            return go == null ? null : go.transform;
        }

        /// <summary>
        /// �������ֻ�ȡ�ֵܽڵ�
        /// </summary>
        /// <param name="transform">ָ����transform</param>
        /// <param name="name">�ֵܽڵ������</param>
        /// <returns>�ֵܽڵ�</returns>
        public static Transform GetBrother(this Transform transform, string name)
        {
            GameObject go = transform.gameObject.GetBrother(name);
            return go == null ? null : go.transform;
        }

        /// <summary>
        /// ����������Transform
        /// </summary>
        /// <param name="transform">ָ����transform</param>
        /// <param name="name">�����������</param>
        /// <returns>�������Transform</returns>
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