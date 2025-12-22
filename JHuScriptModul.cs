using Rocket.Core.Logging;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using uScript.API.Attributes;
using uScript.Module.Main.Classes;
using uScript.Unturned;

namespace JHuScript
{
    public class JHuScriptModul : ScriptModuleBase
    {
        protected override void OnModuleLoaded()
        {
            Logger.Log("JHuScriptModul loaded", ConsoleColor.White);
            Logger.Log("VehicleMan.GetGuid(vehicleInstanceId); VehicleMan.Spawn(Guid, pos, angle); VehicleMan.SpawnLocked(Guid, pos, angle, playerOwnerId); VehicleMan.SpawnLocked(vehicle, pos, angle, player); VehicleMan.SpawnLockedByInstance(instanceId, pos, angle, playerOwnerId); VehicleMan.SetColor(vehicle, hexColor); VehicleMan.SetColor(instanceId, hexColor); VehicleMan.SetRandomColor(vehicle); VehicleMan.SetRandomColor(instanceId); VehicleMan.CopyColor(copyFrom, copyTo);");
            Logger.Log("VehicleMan.AddPlayer(vehicle, player); VehicleMan.AddPlayer(instanceId, playerId); VehicleMan.AddPlayer(instanceId, playerId, seat);");
            Logger.Log("VehicleMan.Teleport(vehicle, Pos, Rot); VehicleMan.Teleport(instanceId, Pos, Rot); VehicleMan.Teleport(vehicle, player);");
        }
    }
    [ScriptModule("VehicleMan")]
    public class VehicleMan
    {
        [ScriptFunction("Spawn")]
        public static VehicleClass Spawn(string text, Vector3Class position, float angle = 0)
        {
            Asset asset = ResolveVehicle(text);
            if (asset == null)
            {
                return null;
            }
            InteractableVehicle intVeh = VehicleManager.spawnVehicleV2(asset, position.Vector3, UnityEngine.Quaternion.Euler(0f, angle, 0f));
            if (intVeh == null)
            {
                return null;
            }
            return new VehicleClass(intVeh);
        }
        [ScriptFunction("SpawnLocked")]
        public static VehicleClass SpawnLocked(string text, Vector3Class position, float angle = 0, string playerId = "")
        {
            Asset asset = ResolveVehicle(text);
            if (asset == null)
            {
                return null;
            }
            InteractableVehicle intVeh = VehicleManager.spawnVehicleV2(asset, position.Vector3, UnityEngine.Quaternion.Euler(0f, angle, 0f));
            if (intVeh == null)
            {
                return null;
            }
            intVeh.tellLocked(new Steamworks.CSteamID(ulong.Parse(playerId)), Steamworks.CSteamID.Nil, true);
            return new VehicleClass(intVeh);
        }
        [ScriptFunction("SpawnLocked")]
        public static VehicleClass SpawnLocked(VehicleClass veh, Vector3Class position, float angle, PlayerClass player)
        {
            if (veh == null || veh.Vehicle == null || veh.Vehicle.asset == null)
                return null;

            InteractableVehicle intVeh = VehicleManager.spawnVehicleV2(
                veh.Vehicle.asset,
                position.Vector3,
                UnityEngine.Quaternion.Euler(0f, angle, 0f)
            );

            if (intVeh == null)
                return null;

            if (ulong.TryParse(player.Id, out ulong steamId))
            {
                if (ulong.TryParse(player.SteamGroup, out ulong groupId))
                {
                    intVeh.tellLocked(new CSteamID(steamId), new CSteamID(groupId), true);
                }
                else
                {
                    intVeh.tellLocked(new CSteamID(steamId), CSteamID.Nil, true);
                }
            }
            VehicleClass b = new VehicleClass(intVeh);
            CopyColor(veh, b);
            return b;
        }
        [ScriptFunction("SpawnLockedByInstance")]
        public static VehicleClass SpawnLockedByInstance(uint instanceId, Vector3Class position, float angle, string playerId)
        {
            var veh = VehicleManager.getVehicle(instanceId);
            if (veh == null || veh.asset == null)
                return null;

            InteractableVehicle intVeh = VehicleManager.spawnVehicleV2(
                veh.asset,
                position.Vector3,
                UnityEngine.Quaternion.Euler(0f, angle, 0f)
            );

            if (intVeh == null)
                return null;

            if (ulong.TryParse(playerId, out ulong steamId))
                intVeh.tellLocked(new CSteamID(steamId), CSteamID.Nil, true);

            return new VehicleClass(intVeh);
        }
        [ScriptFunction("SpawnLockedByInstance")]
        public static VehicleClass SpawnLockedByInstance(uint instanceId, Vector3Class position, float angle, PlayerClass player)
        {
            var veh = VehicleManager.getVehicle(instanceId);
            if (veh == null || veh.asset == null)
                return null;

            InteractableVehicle intVeh = VehicleManager.spawnVehicleV2(
                veh.asset,
                position.Vector3,
                UnityEngine.Quaternion.Euler(0f, angle, 0f)
            );

            if (intVeh == null)
                return null;

            if (ulong.TryParse(player.Id, out ulong steamId))
            {
                if(ulong.TryParse(player.SteamGroup, out ulong groupId))
                {
                    intVeh.tellLocked(new CSteamID(steamId), new CSteamID(groupId), true);
                }
                else
                {
                    intVeh.tellLocked(new CSteamID(steamId), CSteamID.Nil, true);
                }
            }

            return new VehicleClass(intVeh);
        }
        [ScriptFunction("CopyColor")]
        public static void CopyColor(VehicleClass a, VehicleClass b)
        {
            if (!a.Vehicle.asset.SupportsPaintColor || !b.Vehicle.asset.SupportsPaintColor) return;
            b.Vehicle.ServerSetPaintColor(a.Vehicle.PaintColor);
        }

        [ScriptFunction("GetGuid")]
        public static string GetGuid(uint instanceId)
        {
            InteractableVehicle veh = VehicleManager.getVehicle(instanceId);
            if (veh == null) return "";
            if (veh.asset.GUID == null) return veh.asset.id.ToString();
            return veh.asset.GUID.ToString();
        }
        [ScriptFunction("SetColor")]
        public static bool SetColor(VehicleClass veh, string color)
        {
            if (!veh.Vehicle.asset.SupportsPaintColor) return false;
            veh.Vehicle.ServerSetPaintColor((UnityEngine.Color)Rocket.Unturned.Chat.UnturnedChat.GetColorFromHex(color));
            return true;
        }
        [ScriptFunction("SetColor")]
        public static bool SetColor(uint instanceId, string color)
        {
            InteractableVehicle veh = VehicleManager.getVehicle(instanceId);
            if (veh == null) return false;
            if (!veh.asset.SupportsPaintColor) return false;
            veh.ServerSetPaintColor((UnityEngine.Color)Rocket.Unturned.Chat.UnturnedChat.GetColorFromHex(color));
            return true;
        }
        [ScriptFunction("SetRandomColor")]
        public static bool SetRandomColor(VehicleClass veh)
        {
            if (!veh.Vehicle.asset.SupportsPaintColor) return false;
            veh.Vehicle.ServerSetPaintColor((UnityEngine.Color32)veh.Vehicle.asset.GetRandomDefaultPaintColor());
            return true;
        }
        [ScriptFunction("SetRandomColor")]
        public static bool SetRandomColor(uint instanceId)
        {
            InteractableVehicle veh = VehicleManager.getVehicle(instanceId);
            if (veh == null) return false;
            if (!veh.asset.SupportsPaintColor) return false;
            veh.ServerSetPaintColor((UnityEngine.Color32)veh.asset.GetRandomDefaultPaintColor());
            return true;
        }
        [ScriptFunction("AddPlayer")]
        public static bool AddPlayer(VehicleClass veh, PlayerClass Pl)
        {
            return VehicleManager.ServerForcePassengerIntoVehicle(Pl.Player, veh.Vehicle);
        }
        [ScriptFunction("AddPlayer")]
        public static bool AddPlayer(uint instanceId, string playerId)
        {
            InteractableVehicle veh = VehicleManager.getVehicle(instanceId);
            if (veh == null) return false;
            Rocket.Unturned.Player.UnturnedPlayer Pl = Rocket.Unturned.Player.UnturnedPlayer.FromCSteamID(new CSteamID(ulong.Parse(playerId)));
            if (Pl == null) return false;
            return VehicleManager.ServerForcePassengerIntoVehicle(Pl.Player, veh);
        }
        [ScriptFunction("AddPlayer")]
        public static bool AddPlayer(uint instanceId, string playerId, byte seat)
        {
            InteractableVehicle veh = VehicleManager.getVehicle(instanceId);
            if (veh == null) return false;
            Rocket.Unturned.Player.UnturnedPlayer Pl = Rocket.Unturned.Player.UnturnedPlayer.FromCSteamID(new CSteamID(ulong.Parse(playerId)));
            if (Pl == null) return false;
            if (!VehicleManager.ServerForcePassengerIntoVehicle(Pl.Player, veh)) return false;
            if(veh.trySwapPlayer(Pl.Player, seat, out byte _seat) && _seat != seat)
            {
                VehicleManager.ReceiveSwapVehicleSeats(instanceId, _seat, seat);
            }
            return true;
        }
        [ScriptFunction("Teleport")]
        public static bool Teleport(uint instanceId, Vector3Class Pos, Vector3Class Rot)
        {
            InteractableVehicle veh = VehicleManager.getVehicle(instanceId);
            if (veh == null) return false;
            UnityEngine.Quaternion rotat = new UnityEngine.Quaternion(Rot.X, Rot.Y, Rot.Z, Rot.Vector3.magnitude);
            veh.transform.SetPositionAndRotation(Pos.Vector3, rotat);
            return true;
        }
        [ScriptFunction("Teleport")]
        public static void Teleport(VehicleClass Veh, Vector3Class Pos, Vector3Class Rot)
        {
            Veh.Vehicle.transform.SetPositionAndRotation(Pos.Vector3, new UnityEngine.Quaternion(Rot.X, Rot.Y, Rot.Z, Rot.Vector3.magnitude));
        }
        [ScriptFunction("Teleport")]
        public static void Teleport(VehicleClass Veh, Vector3Class Pos, byte angle)
        {
            Veh.Vehicle.transform.SetPositionAndRotation(Pos.Vector3, UnityEngine.Quaternion.Euler(0, angle, 0));
        }
        [ScriptFunction("Teleport")]
        public static void Teleport(VehicleClass Veh, PlayerClass Player)
        {
            Veh.Vehicle.transform.SetPositionAndRotation(Player.Position.Vector3, Player.Player.look.transform.rotation);
        }

        private static VehicleAsset ResolveVehicle(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            if (Guid.TryParse(text, out var guid))
                return Assets.find(guid) as VehicleAsset;

            if (ushort.TryParse(text, out var id))
                return Assets.find(EAssetType.VEHICLE, id) as VehicleAsset;

            return (VehicleAsset)FindByString(text);
        }
        public static Asset FindByString(string input)
        {
            input = input.Trim();
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var list = new List<VehicleAsset>();
            Assets.find(list);

            foreach (VehicleAsset item in list)
            {
                if (string.Equals(input, item.name, StringComparison.InvariantCultureIgnoreCase) ||
                    string.Equals(input, item.vehicleName, StringComparison.InvariantCultureIgnoreCase) ||
                    item.name.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    item.vehicleName.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
