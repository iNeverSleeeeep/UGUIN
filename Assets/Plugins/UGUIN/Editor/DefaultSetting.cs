using System;
using UnityEngine;

namespace UGUIN
{
    [CreateAssetMenu(fileName ="UGUIN Default Setting.asset", menuName ="UGUIN/CreateDefaultSettingAsset", order=0)]
    public class DefaultSetting : ScriptableObject
    {
        public string Text;


#if UNITY_EDITOR
        public void SetDefaultTextValues(INText text)
        {

        }
#endif
    }
}
