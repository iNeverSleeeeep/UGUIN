using UnityEngine;
using UnityEditor;

namespace UGUIN
{
    public static class DefaultControls
    {        /// <summary>
             /// Object used to pass resources to use for the default controls.
             /// </summary>
        public struct Resources
        {
            /// <summary>
            /// The primary sprite to be used for graphical UI elements, used by the button, toggle, and dropdown controls, among others.
            /// </summary>
            public Sprite standard;

            /// <summary>
            /// Sprite used for background elements.
            /// </summary>
            public Sprite background;

            /// <summary>
            /// Sprite used as background for input fields.
            /// </summary>
            public Sprite inputField;

            /// <summary>
            /// Sprite used for knobs that can be dragged, such as on a slider.
            /// </summary>
            public Sprite knob;

            /// <summary>
            /// Sprite used for representation of an "on" state when present, such as a checkmark.
            /// </summary>
            public Sprite checkmark;

            /// <summary>
            /// Sprite used to indicate that a button will open a dropdown when clicked.
            /// </summary>
            public Sprite dropdown;

            /// <summary>
            /// Sprite used for masking purposes, for example to be used for the viewport of a scroll view.
            /// </summary>
            public Sprite mask;
        }

        private readonly static string m_SettingPath = "Assets/Plugins/UGUIN/Editor/Resources/UGUIN Default Setting.asset";
        private static DefaultSetting s_DefaultSettingInstance;
        private static DefaultSetting s_DefaultSetting
        {
            get
            {
                if (s_DefaultSettingInstance == null)
                    s_DefaultSettingInstance = AssetDatabase.LoadAssetAtPath<DefaultSetting>(m_SettingPath);
                
                return s_DefaultSettingInstance;
            }
        }

        private const float kWidth = 160f;
        private const float kThickHeight = 30f;
        private const float kThinHeight = 20f;
        private static Vector2 s_ThickElementSize = new Vector2(kWidth, kThickHeight);
        private static Vector2 s_ThinElementSize = new Vector2(kWidth, kThinHeight);
        private static Vector2 s_ImageElementSize = new Vector2(100f, 100f);
        private static Color s_DefaultSelectableColor = new Color(1f, 1f, 1f, 1f);
        private static Color s_PanelColor = new Color(1f, 1f, 1f, 0.392f);
        private static Color s_TextColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);

        private static GameObject CreateUIElementRoot(string name, Vector2 size)
        {
            GameObject child = new GameObject(name);
            RectTransform rectTransform = child.AddComponent<RectTransform>();
            rectTransform.sizeDelta = size;
            return child;
        }

        /// <summary>
        /// Create the basic UI Text.
        /// </summary>
        /// <remarks>
        /// Hierarchy:
        /// (root)
        ///     Text
        /// </remarks>
        /// <param name="resources">The resources to use for creation.</param>
        /// <returns>The root GameObject of the created element.</returns>
        public static GameObject CreateText(Resources resources)
        {
            GameObject go = CreateUIElementRoot("Text", s_ThickElementSize);

            INText lbl = go.AddComponent<INText>();
            SetDefaultTextValues(lbl);

            return go;
        }


        private static void SetDefaultTextValues(INText lbl)
        {
            if (s_DefaultSetting == null)
            {
                // Set text values we want across UI elements in default controls.
                // Don't set values which are the same as the default values for the Text component,
                // since there's no point in that, and it's good to keep them as consistent as possible.
                lbl.color = s_TextColor;

                // Reset() is not called when playing. We still want the default font to be assigned
                lbl.font = UnityEngine.Resources.GetBuiltinResource<Font>("Arial.ttf");
            }
            else
            {
                EditorJsonUtility.FromJsonOverwrite(s_DefaultSetting.Text, lbl);
            }
        }
    }
}
