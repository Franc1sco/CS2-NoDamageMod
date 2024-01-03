using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using System.Text.Json.Serialization;

namespace NoDamageMod;

public class ConfigGen : BasePluginConfig
{
    [JsonPropertyName("EnabledDamageByDefault")] public bool Enabled { get; set; } = false;
    [JsonPropertyName("CommandAccessFlag")] public string CommandAccessFlag { get; set; } = "@css/vip";
}

[MinimumApiVersion(142)]
public class NoDamageMod : BasePlugin, IPluginConfig<ConfigGen>
{
    public override string ModuleName => "No Damage Mod";
    public override string ModuleAuthor => "Franc1sco Franug";
    public override string ModuleVersion => "1.1";
    public ConfigGen Config { get; set; } = null!;
    public void OnConfigParsed(ConfigGen config) { Config = config; }
    private bool bDamageEnabled;

    public override void Load(bool hotReload)
    {
        bDamageEnabled = Config.Enabled;
        VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Hook(OnTakeDamage, HookMode.Pre);
    }

    public override void Unload(bool hotReload)
    {
        VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Unhook(OnTakeDamage, HookMode.Pre);
    }

    [ConsoleCommand("css_damage", "")]
    public void CommandDamage(CCSPlayerController? player, CommandInfo info)
    {
        if (player == null || !player.IsValid)
        {
            return;
        }

        if (Config.CommandAccessFlag != "" && !AdminManager.PlayerHasPermissions(player, Config.CommandAccessFlag))
        {
            info.ReplyToCommand("You dont have access for this command");
            return;
        }

        bDamageEnabled = !bDamageEnabled;

        if (bDamageEnabled) info.ReplyToCommand("Damage Enabled");
        else info.ReplyToCommand("Damage Disabled");
    }

    HookResult OnTakeDamage(DynamicHook handle)
    {
        CEntityInstance victim = handle.GetParam<CEntityInstance>(0);

        if (victim != null && !bDamageEnabled)
        {
            return HookResult.Handled;
        }

        return HookResult.Continue;
    }
}

