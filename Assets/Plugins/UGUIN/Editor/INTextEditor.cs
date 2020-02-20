using UnityEngine;
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

        SerializedProperty m_FontData;

        protected override void OnEnable()
        {
            if (target == null)
                return;
            base.OnEnable();
            m_RawText = serializedObject.FindProperty("m_RawText");
            m_Text = serializedObject.FindProperty("m_Text");
            m_I18NText = serializedObject.FindProperty("m_I18NText");

            m_FontData = serializedObject.FindProperty("m_FontData");
        }

        public override void OnInspectorGUI()
        {
            if (target == null)
                return;
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(m_RawText);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(m_Text);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(m_FontData);

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
