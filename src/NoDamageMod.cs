using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;

namespace NoDamageMod;


[MinimumApiVersion(142)]
public class NoDamageMod : BasePlugin
{
    public override string ModuleName => "No Damage Mod";
    public override string ModuleAuthor => "Franc1sco Franug";
    public override string ModuleVersion => "1.0";

    public override void Load(bool hotReload)
    {
        VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Hook(OnTakeDamage, HookMode.Pre);
    }

    public override void Unload(bool hotReload)
    {
        VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Unhook(OnTakeDamage, HookMode.Pre);
    }

    HookResult OnTakeDamage(DynamicHook handle)
    {
        CEntityInstance victim = handle.GetParam<CEntityInstance>(0);

        if (victim != null)
        {
            return HookResult.Handled;
        }

        return HookResult.Continue;
    }
}

