using UnityEngine;
using UnityEditor;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace UGUIN
{
    [CustomEditor(typeof(DefaultSetting))]
    public class DefaultSettingEditor : Editor
    {
        class SerializerContext
        {
            public Component component;
            public SerializedProperty serializedProperty;
            public Editor editor;
            public bool state;
        }

        Dictionary<Type, SerializerContext> m_SerializeComponents = new Dictionary<Type, SerializerContext>();
        Dictionary<Type, string> m_SerializeTypes = new Dictionary<Type, string>
        {
            { typeof(INText), "Text" }
        };
        XmlSerializer m_TextSerializer;

        GameObject m_TempRoot;

        private void OnEnable()
        {
            serializedObject.Update();

            m_TempRoot = new GameObject("TempRoot");
            m_TempRoot.hideFlags = HideFlags.HideAndDontSave;
            foreach (var st in m_SerializeTypes)
            {
                var go = new GameObject(st.Key.Name);
                go.transform.parent = m_TempRoot.transform;
                var component = go.AddComponent(st.Key);
                var property = serializedObject.FindProperty(st.Value);
                var ms = new MemoryStream();

                if (string.IsNullOrEmpty(property.stringValue) == false)
                {
                    EditorJsonUtility.FromJsonOverwrite(property.stringValue, component);
                }
                else
                {
                    property.stringValue = EditorJsonUtility.ToJson(component);
                }
                var context = new SerializerContext();
                context.editor = Editor.CreateEditor(component);
                context.component = component;
                context.serializedProperty = property;
                m_SerializeComponents.Add(st.Key, context);
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void OnDisable()
        {
            DestroyImmediate(m_TempRoot);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            foreach (var st in m_SerializeTypes)
            {
                var context = m_SerializeComponents[st.Key];
                context.state = EditorUtilities.DrawHeader(st.Key.Name, context.state);
                if (context.state)
                {
                    EditorGUI.BeginChangeCheck();
                    context.editor.OnInspectorGUI();
                    if (EditorGUI.EndChangeCheck())
                    {
                        context.serializedProperty.stringValue = EditorJsonUtility.ToJson(context.editor.target);
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
