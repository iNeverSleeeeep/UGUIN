using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGUIN
{
	public static class I18N
	{
		private static Dictionary<string, string> LookupDictionay = new Dictionary<string, string>();

        public static string Lookup(string rawtext)
		{
			var text = rawtext;
			if (LookupDictionay.TryGetValue(rawtext, out text))
				return text;
			return rawtext;
		}

        public static void Load(string path)
		{
#if UNITY_EDITOR
            var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            if (textAsset != null)
            {
                var pairList = textAsset.text.Split('\n');
                foreach (var pair in pairList)
                {
                    var indexOfSep = pair.IndexOf(';');
                    var key = pair.Substring(0, indexOfSep);
                    var value = pair.Substring(indexOfSep + 1);
                    LookupDictionay[key] = value;
                }
            }
#else
            // 这里自己按照项目不同来实现
#endif
            I18NTracker.OnChangeRequested();
        }
	}
}
