using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UGUIN
{
    [AddComponentMenu("UIN/Text", 30)]
    public class INText : Text, ILocaleChangeListener
	{
        [TextArea(3, 10)] [SerializeField] protected string m_RawText = string.Empty;
        [SerializeField] private bool m_I18NText = true;

        public string rawtext
		{
            get
            {
                return m_RawText;
            }
            set
            {
                text = value;
            }
		}

        protected override void OnEnable()
        {
            base.OnEnable();
            I18NTracker.Track(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            I18NTracker.UnTrack(this);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            refresh();
        }
#endif

        public override string text
        {
            get
            {
                return m_Text;
            }
            set
            {
                if (m_RawText == value)
                    return;
                m_RawText = value;
                refresh();
            }
        }

        void ILocaleChangeListener.OnLocaleChanged()
        {
            refresh();
        }

        void refresh()
        {
            if (m_I18NText)
                base.text = I18N.Lookup(m_RawText);
            else
                base.text = m_RawText;
        }
    }
}

