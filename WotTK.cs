using Terraria;
using Terraria.ModLoader;

namespace WotTK
{
	public class WotTK : Mod
	{
		public static WotTK Instance;
        public override void Load()
        {
            Instance = this;
        }
        public override void Unload()
        {
            Instance = null;
        }
    }
}