using BepInEx;
using System.Security.Permissions;

// Allows access to private members
#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618

namespace SuperJump;

[BepInPlugin("author.superjump", "Super Jump", "1.0.0")]
sealed class Plugin : BaseUnityPlugin
{
    bool init;

    public void OnEnable()
    {
        On.RainWorld.OnModsInit += OnModsInit;
    }

    private void OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        orig(self);

        if (init) return;
        init = true;

        MachineConnector.SetRegisteredOI("author.superjump", Options.instance);

        On.Player.Update += Player_Update;
        On.SlugcatStats.ctor += SlugcatStats_ctor;

        Logger.LogDebug("SuperJump: hooks applied");
    }

    private void Player_Update(On.Player.orig_Update orig, Player self, bool eu)
    {
        orig(self, eu);

        if (self.dead || self.isNPC) return;

        bool jumpPressed = self.input[0].jmp && !self.input[1].jmp;
        bool standing = self.bodyMode == Player.BodyModeIndex.Stand;

        if (jumpPressed && standing)
        {
            self.bodyChunk.vel.y += Options.instance.jumpPower.Value * 1.2f;
        }
    }

    private void SlugcatStats_ctor(On.SlugcatStats.orig_ctor orig, SlugcatStats self, SlugcatStats.Name slugcat, bool malnourished)
    {
        orig(self, slugcat, malnourished);

        if (Options.instance.speedBoost.Value)
        {
            self.runspeedFac *= 1.4f;
            self.poleClimbSpeed *= 1.3f;
            self.corridorClimbSpeed *= 1.3f;
        }
    }
}
