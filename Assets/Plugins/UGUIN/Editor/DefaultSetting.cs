using System;
using UnityEngine;

namespace UGUIN
{
    [CreateAssetMenu(fileName ="GUINDefault.asset", menuName ="UGUIN/CreateDefaultSettingAsset", order=0)]
    public class DefaultSetting : ScriptableObject
    {
        public INText Text;
    }
}
