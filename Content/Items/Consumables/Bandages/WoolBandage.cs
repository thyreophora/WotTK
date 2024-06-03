using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotTK.Content.Items.Consumables.Abstract;

namespace WotTK.Content.Items.Consumables.Bandages
{
    public class WoolBandage : Bandage
    {
        public override void SetDefaults()
        {
            DefaultBandage(7, 161);
        }
    }
}
