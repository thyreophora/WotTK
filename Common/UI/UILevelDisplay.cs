using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using WotTK.Common.Globals;
using WotTK.Common.Players;

namespace WotTK.Common.UI;

public sealed class UILevelDisplay : UIState
{
    private const float ElementPadding = 8f;
    private const float BorderPadding = 4f;

    private static readonly Asset<Texture2D> UnknownTexture = ModContent.Request<Texture2D>("WotTK/Common/UI/Unknown", AssetRequestMode.ImmediateLoad);

    private static readonly Asset<Texture2D> SkullTexture = ModContent.Request<Texture2D>("WotTK/Common/UI/Skull", AssetRequestMode.ImmediateLoad);

    private static readonly Asset<Texture2D> LevelTexture = ModContent.Request<Texture2D>("WotTK/Common/UI/Level", AssetRequestMode.ImmediateLoad);

    private static readonly Asset<Texture2D> PanelLeftTexture = ModContent.Request<Texture2D>("WotTK/Common/UI/Panel_Left", AssetRequestMode.ImmediateLoad);
    private static readonly Asset<Texture2D> PanelTexture = ModContent.Request<Texture2D>("WotTK/Common/UI/Panel", AssetRequestMode.ImmediateLoad);
    private static readonly Asset<Texture2D> PanelRightTexture = ModContent.Request<Texture2D>("WotTK/Common/UI/Panel_Right", AssetRequestMode.ImmediateLoad);

    // Splits PascalCase and numbers to look more presentable.
    private static readonly Regex Pattern = new("(\\B[A-Z0-9])", RegexOptions.Compiled);

    private Player Player => Main.LocalPlayer;

    public UIElement Area { get; private set; }

    public UIImage Panel { get; private set; }
    public UIImage PanelLeft { get; private set; }
    public UIImage PanelRight { get; private set; }

    public UIText NPCName { get; private set; }
    public UIText NPCLevel { get; private set; }

    public UIImage LevelImage { get; private set; }
    public UIImage SkullImage { get; private set; }

    public UIImageFramed NPCImage { get; private set; }

    public float Opacity { get; private set; }
    
    public Color NPCColor { get; private set; }
    public Color LevelColor { get; private set; }
    public Color SkullColor { get; private set; }

    public override void OnInitialize() {
        base.OnInitialize();

        Area = new UIElement {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(24f),
            Height = StyleDimension.FromPixels(48f)
        };

        Append(Area);

        PanelLeft = new UIImage(PanelLeftTexture) {
            Height = StyleDimension.FromPercent(1f),
            OverrideSamplerState = SamplerState.PointClamp
        };

        Area.Append(PanelLeft);

        Panel = new UIImage(PanelTexture) {
            OverflowHidden = true,
            ScaleToFit = true,
            Left = StyleDimension.FromPixels(BorderPadding),
            Height = StyleDimension.FromPercent(1f),
            OverrideSamplerState = SamplerState.PointClamp
        };

        Area.Append(Panel);

        PanelRight = new UIImage(PanelRightTexture) {
            HAlign = 1f,
            Height = StyleDimension.FromPercent(1f),
            OverrideSamplerState = SamplerState.PointClamp
        };

        Area.Append(PanelRight);

        NPCName = new UIText(string.Empty, 0.8f) {
            Left = StyleDimension.FromPixels(40f),
            Top = StyleDimension.FromPixels(ElementPadding),
            OverrideSamplerState = SamplerState.PointClamp
        };

        Panel.Append(NPCName);

        NPCLevel = new UIText(string.Empty, 0.6f) {
            Left = StyleDimension.FromPixels(40f),
            Top = StyleDimension.FromPixels(-ElementPadding),
            VAlign = 1f,
            OverrideSamplerState = SamplerState.PointClamp
        };

        Panel.Append(NPCLevel);

        LevelImage = new UIImage(LevelTexture) {
            ScaleToFit = true,
            VAlign = 1f,
            Left = StyleDimension.FromPixels(2f),
            Height = StyleDimension.FromPixels(2f),
            Width = StyleDimension.FromPixelsAndPercent(-4f, 1f),
            OverrideSamplerState = SamplerState.PointClamp
        };

        Area.Append(LevelImage);
        
        NPCImage = new UIImageFramed(UnknownTexture, UnknownTexture.Frame())
        {
            Left = StyleDimension.FromPixels(ElementPadding),
            VAlign = 0.5f,
            OverrideSamplerState = SamplerState.PointClamp
        };
        
        Panel.Append(NPCImage);
        
        SkullImage = new UIImage(SkullTexture)
        {
            HAlign = 1f,
            VAlign = 0.5f,
            Left = StyleDimension.FromPixels(-ElementPadding),
            OverrideSamplerState = SamplerState.PointClamp
        };
        
        Panel.Append(SkullImage);
    }
    
    public override void Draw(SpriteBatch spriteBatch) {
        PlayerInput.SetZoom_World();

        spriteBatch.End();
        spriteBatch.Begin(default, default, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
        
        UpdateColors();
        UpdateElements();
        
        base.Draw(spriteBatch);

        PlayerInput.SetZoom_UI();

        spriteBatch.End();
        spriteBatch.Begin(default, default, default, default, default, default, Main.UIScaleMatrix);
    }
    
    private void UpdateColors() {
        PanelLeft.Color = Color.White * Opacity;
        Panel.Color = Color.White * Opacity;
        PanelRight.Color = Color.White * Opacity;

        NPCName.TextColor = Color.White * Opacity;
        NPCLevel.TextColor = Color.Lerp(NPCLevel.TextColor, LevelColor, 0.1f) * Opacity;

        LevelImage.Color = Color.Lerp(LevelImage.Color, LevelColor, 0.1f) * Opacity;
        SkullImage.Color = Color.Lerp(SkullImage.Color, SkullColor, 0.1f) * Opacity;
        
        NPCImage.Color = Color.Lerp(NPCImage.Color, NPCColor, 0.1f) * Opacity;
    }

    private void ResetElements() {
        Opacity = MathHelper.Lerp(Opacity, 0f, 0.2f);

        NPCName.SetText(string.Empty);
        NPCLevel.SetText(string.Empty);
        
        NPCImage.SetImage(UnknownTexture, UnknownTexture.Frame());
    }

    private void UpdateElements() {
        var foundNPC = TryGetMouseNPC(out var npc);
        var foundNPCName = NPCID.Search.TryGetName(NPCID.FromNetId(npc.type), out var name);
        var foundNPCGlobal = npc.TryGetGlobalNPC<NPCLevels>(out var globalNPC);
        
        if (!foundNPC || !foundNPCName || !foundNPCGlobal) {
            ResetElements();
            return;
        }
        
        var level = globalNPC.Level;

        if (level <= 0) {
            ResetElements();
            return;
        }
        
        Opacity = MathHelper.Lerp(Opacity, 1f, 0.2f);
        
        UpdateLevel(level);

        var font = FontAssets.MouseText.Value;

        var npcText = Pattern.Replace(name, " $1");
        var npcTextSize = font.MeasureString(npcText);

        var levelText = $"Level: {level}";
        var levelTextSize = font.MeasureString(levelText);
        
        NPCName.SetText(npcText);
        NPCLevel.SetText(levelText);

        var frame = new Rectangle(npc.frame.X, npc.frame.Y, 24, 24);
        
        NPCImage.SetImage(TextureAssets.Npc[npc.type], frame);

        if (npc.color != Color.Transparent) {
            NPCColor = npc.GetAlpha(npc.color);
        }
        else {
            NPCColor = npc.GetAlpha(Color.White);
        }

        var desiredWidth = MathF.Max(npcTextSize.X, levelTextSize.X);
        var width = MathHelper.Lerp(Panel.Width.Pixels, desiredWidth, 0.2f) + frame.Width + BorderPadding;
        var ceiledWidth = MathF.Ceiling(width);

        Area.Width.Set(ceiledWidth, 0f);
        Panel.Width.Set(ceiledWidth - BorderPadding * 2f, 0f);

        Recalculate();
    }

    private void UpdateLevel(int level) {
        if (!Player.TryGetModPlayer<WotTKPlayer>(out var modPlayer)) {
            return;
        }

        var difference = Math.Abs(modPlayer.playerLevel - level);
        
        switch (difference) {
            case >= 4:
                LevelColor = Color.Gray;
                SkullColor = Color.White;
                break;
            case 3:
                LevelColor = Color.Red;
                SkullColor = Color.Transparent;
                break;
            case 2:
                LevelColor = Color.Lime;
                SkullColor = Color.Transparent;
                break;
            case 1:
                LevelColor = Color.Yellow;
                SkullColor = Color.Transparent;
                break;
            case 0:
                LevelColor = Color.Orange;
                SkullColor = Color.Transparent;
                break;
        }
    }

    private static bool TryGetMouseNPC([MaybeNullWhen(false)] out NPC? npc) {
        var foundNPC = false;

        npc = null;
        
        for (var i = 0; i < Main.maxNPCs; i++) {
            npc = Main.npc[i];

            if (!npc.active) {
                continue;
            }
            
            Main.instance.LoadNPC(npc.type);

            npc.position += npc.netOffset;

            var zoom = Main.GameViewMatrix.Zoom;
            
            var mousePosition = new Rectangle((int)((float)Main.mouseX + Main.screenPosition.X), (int)((float)Main.mouseY + Main.screenPosition.Y), 1, 1);

            var npcHitbox = new Rectangle((int)npc.Bottom.X - npc.frame.Width / 2, (int)npc.Bottom.Y - npc.frame.Height, npc.frame.Width, npc.frame.Height);
            var mouseHitbox = new Rectangle(mousePosition.X, mousePosition.Y, 1, 1);

            NPCLoader.ModifyHoverBoundingBox(npc, ref npcHitbox);

            if (mouseHitbox.Intersects(npcHitbox)) {
                npc.position -= npc.netOffset;
                foundNPC = true;
                break;
            }
            
            npc.position -= npc.netOffset;
        }

        return foundNPC;
    }
}
