﻿using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace UGUIN
{
    [CustomEditor(typeof(INText), true)]
    [CanEditMultipleObjects]
    public class INTextEditor : UnityEditor.UI.TextEditor
    {
        SerializedProperty m_RawText;
        SerializedProperty m_Text;
        SerializedProperty m_I18NText;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_RawText = serializedObject.FindProperty("m_RawText");
            m_Text = serializedObject.FindProperty("m_Text");
            m_I18NText = serializedObject.FindProperty("m_I18NText");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(m_RawText);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(m_Text);
            EditorGUI.EndDisabledGroup();

            AppearanceControlsGUI();
            RaycastControlsGUI();
            EditorGUILayout.PropertyField(m_I18NText);
            if (EditorGUI.EndChangeCheck())
            {
                if (m_I18NText.boolValue)
                    m_Text.stringValue = I18N.Lookup(m_RawText.stringValue);
                else
                    m_Text.stringValue = m_RawText.stringValue;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
