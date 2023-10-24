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
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Melee)
                return true;
            return false;
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic || damageClass == DamageClass.Melee)
                return StatInheritanceData.Full;
            return StatInheritanceData.None;
        }
    }
}
