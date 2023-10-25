using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace WotTK
{
    public class PaladinDamageType : DamageClass
    {
        internal static PaladinDamageType Instance;
        public override void Load() => PaladinDamageType.Instance = this;
        public override void Unload() => PaladinDamageType.Instance = null;
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            /*if (damageClass == DamageClass.Melee || damageClass == DamageClass.MeleeNoSpeed)
                return true;
            return false;*/

            return damageClass == DamageClass.Melee || damageClass == DamageClass.MeleeNoSpeed;
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            /*if (damageClass == DamageClass.Generic || damageClass == DamageClass.Melee)
                return StatInheritanceData.Full;
            return StatInheritanceData.None;*/

            return (damageClass == DamageClass.Generic || damageClass == DamageClass.Melee || damageClass == DamageClass.MeleeNoSpeed) ? StatInheritanceData.Full : StatInheritanceData.None;
        }
    }
}
