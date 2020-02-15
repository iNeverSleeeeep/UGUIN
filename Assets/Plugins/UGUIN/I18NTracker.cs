
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UGUIN
{
    public interface ILocaleChangeListener
    {
        void OnLocaleChanged();
    }

    public static class I18NTracker
    {
        static HashSet<ILocaleChangeListener> m_Tracked = new HashSet<ILocaleChangeListener>();

        public static void Track(ILocaleChangeListener l)
        {
            m_Tracked.Add(l);
        }

        public static void UnTrack(ILocaleChangeListener l)
        {
            m_Tracked.Remove(l);
        }

        internal static void OnChangeRequested()
        {
            foreach (var tracked in m_Tracked)
                tracked.OnLocaleChanged();
        }
    }
}
