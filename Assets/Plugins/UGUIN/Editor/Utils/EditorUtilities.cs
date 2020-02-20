using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGUIN
{
    public static class EditorUtilities
    {
        static Dictionary<string, GUIContent> s_GUIContentCache = new Dictionary<string, GUIContent>();

        static class Styling
        {
            public static GUIStyle headerLabel = new GUIStyle(EditorStyles.miniLabel);

            static readonly Color headerBackgroundDark = new Color(0.1f, 0.1f, 0.1f, 0.2f);
            static readonly Color headerBackgroundLight = new Color(1f, 1f, 1f, 0.2f);
            public static Color headerBackground { get { return EditorGUIUtility.isProSkin ? headerBackgroundDark : headerBackgroundLight; } }

            static readonly Color splitterDark = new Color(0.12f, 0.12f, 0.12f, 1.333f);
            static readonly Color splitterLight = new Color(0.6f, 0.6f, 0.6f, 1.333f);

            /// <summary>
            /// Color of UI splitters.
            /// </summary>
            public static Color splitter { get { return EditorGUIUtility.isProSkin ? splitterDark : splitterLight; } }
        }

        /// <summary>
        /// Draws a header label.
        /// </summary>
        /// <param name="title">The label to display as a header</param>
        public static void DrawHeaderLabel(string title)
        {
            EditorGUILayout.LabelField(title, Styling.headerLabel);
        }

        internal static bool DrawHeader(string title, bool state)
        {
            var backgroundRect = GUILayoutUtility.GetRect(1f, 17f);

            var labelRect = backgroundRect;
            labelRect.xMin += 16f;
            labelRect.xMax -= 20f;

            var foldoutRect = backgroundRect;
            foldoutRect.y += 1f;
            foldoutRect.width = 13f;
            foldoutRect.height = 13f;

            // Background rect should be full-width
            backgroundRect.xMin = 0f;
            backgroundRect.width += 4f;

            // Background
            EditorGUI.DrawRect(backgroundRect, Styling.headerBackground);

            // Title
            EditorGUI.LabelField(labelRect, GetContent(title), EditorStyles.boldLabel);

            // Foldout
            state = GUI.Toggle(foldoutRect, state, GUIContent.none, EditorStyles.foldout);

            var e = Event.current;
            if (e.type == EventType.MouseDown && backgroundRect.Contains(e.mousePosition) && e.button == 0)
            {
                state = !state;
                e.Use();
            }

            return state;
        }

        /// <summary>
        /// Gets a <see cref="GUIContent"/> for the given label and tooltip. These are recycled
        /// internally and help reduce the garbage collector pressure in the editor.
        /// </summary>
        /// <param name="textAndTooltip">The label and tooltip separated by a <c>|</c>
        /// character</param>
        /// <returns>A recycled <see cref="GUIContent"/></returns>
        public static GUIContent GetContent(string textAndTooltip)
        {
            if (string.IsNullOrEmpty(textAndTooltip))
                return GUIContent.none;

            GUIContent content;

            if (!s_GUIContentCache.TryGetValue(textAndTooltip, out content))
            {
                var s = textAndTooltip.Split('|');
                content = new GUIContent(s[0]);

                if (s.Length > 1 && !string.IsNullOrEmpty(s[1]))
                    content.tooltip = s[1];

                s_GUIContentCache.Add(textAndTooltip, content);
            }

            return content;
        }

        /// <summary>
        /// Draws a horizontal split line.
        /// </summary>
        public static void DrawSplitter()
        {
            var rect = GUILayoutUtility.GetRect(1f, 1f);

            // Splitter rect should be full-width
            rect.xMin = 0f;
            rect.width += 4f;

            if (Event.current.type != EventType.Repaint)
                return;

            EditorGUI.DrawRect(rect, Styling.splitter);
        }
    }
}
