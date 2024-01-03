# CS2-NoDamageMod

Simple "no damage" plugin for servers like idle where don't want damage or just for be disabled/enabled whenever you want via admin command.

### Requirements
* [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp/) (version 142 or higher)

### Installation

Drag and drop from [releases](https://github.com/Franc1sco/CS2-NoDamageMod/releases) to game/csgo/addons/counterstrikesharp/plugins

### Configuration

Configure the file NoDamageMod.json generated on addons/counterstrikesharp/configs/plugins/NoDamageMod
```json
{
  "EnabledDamageByDefault": false,
  "CommandAccessFlag": "@css/vip",
  "ConfigVersion": 1
}
```
* EnabledDamageByDefault - false: Enable damage by default when load the plugin
* CommandAccessFlag . @css/vip: Access required for enable/disable damage by **css_damage** command (or **!damage** on chat).
