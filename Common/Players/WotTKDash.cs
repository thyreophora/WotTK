using Microsoft.Xna.Framework;
using SteelSeries.GameSense.DeviceZone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WotTK.Common.Globals;

namespace WotTK.Common.Players
{
	public class WotTKDash : ModPlayer
	{

		public const int DashRight = 2;
		public const int DashLeft = 3;

		public const int DashCooldown = 50;
		public const int DashDuration = 5;

		public const float DashVelocity = 8f;

		public int DashDir = -1;

		public bool canDash;
		public int DashDelay = 0;
		public int DashTimer = 0;

		public override void ResetEffects() {

			canDash = false;

			if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15) {
				DashDir = DashRight;
			}
			else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15) {
				DashDir = DashLeft;
			}
			else {
				DashDir = -1;
			}
		}

		public override void PreUpdateMovement() {

			if (CanUseDash() && DashDir != -1 && DashDelay == 0) {
				Vector2 newVelocity = Player.velocity;

				switch (DashDir) {


					case DashLeft when Player.velocity.X > -DashVelocity:
					case DashRight when Player.velocity.X < DashVelocity: {

							float dashDirection = DashDir == DashRight ? 1 : -1;
							newVelocity.X = dashDirection * DashVelocity;
							break;
						}
					default:
						return; 
				}


				DashDelay = DashCooldown;
				DashTimer = DashDuration;
				Player.velocity = newVelocity;


			}

			if (DashDelay > 0)
				DashDelay--;

			if (DashTimer > 0) { 

				Player.eocDash = DashTimer;
				Player.armorEffectDrawShadowEOCShield = true;

				DashTimer--;
			}
		}

		private bool CanUseDash() {
			return canDash
				&& Player.dashType == DashID.None
				&& !Player.setSolar
				&& !Player.mount.Active;
		}
	}
}
