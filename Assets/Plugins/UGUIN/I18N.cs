using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGUIN
{
	public static class I18N
	{
		private static Dictionary<string, string> LookupDictionary = new Dictionary<string, string>();

        public static string Lookup(string rawtext)
		{
			var text = rawtext;
			if (LookupDictionary.TryGetValue(rawtext, out text))
				return text;
			return rawtext;
		}

        public static void Load(Dictionary<string, string> dictionary)
		{
            LookupDictionary = dictionary;
            I18NTracker.OnChangeRequested();
        }
	}
}
