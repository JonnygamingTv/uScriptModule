using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using uScript.API.Attributes;
using uScript.Core;
using uScript.Module.Main.Classes;
using uScript.Unturned;
using Logger = Rocket.Core.Logging.Logger;

namespace JHuScript
{
    public class JHuScriptModul : ScriptModuleBase
    {
        protected override void OnModuleLoaded()
        {
            Logger.Log("JHuScriptModul loaded", ConsoleColor.White);
            Logger.Log("VehicleMan.GetGuid(vehicleInstanceId); VehicleMan.Spawn(Guid, pos, angle); VehicleMan.SpawnLocked(Guid, pos, angle, playerOwnerId); VehicleMan.SpawnLocked(vehicle, pos, angle, player); VehicleMan.SpawnLocked(instanceId, pos, angle, player); VehicleMan.SpawnLockedByInstance(instanceId, pos, angle, playerOwnerId); VehicleMan.SetColor(vehicle, hexColor); VehicleMan.SetColor(instanceId, hexColor); VehicleMan.SetRandomColor(vehicle); VehicleMan.SetRandomColor(instanceId); VehicleMan.CopyColor(copyFrom, copyTo);");
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
        [ScriptFunction("SpawnLocked")]
        public static VehicleClass SpawnLocked(uint instanceId, Vector3Class position, float angle, PlayerClass player)
        {
            InteractableVehicle veh = VehicleManager.getVehicle(instanceId);
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
            CopyColor(new VehicleClass(veh), b);
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
    [ScriptModule("BarrMan")]
    public class BarrMan
    {
        [ScriptFunction("CopyOwnerToClosest")]
        public static void CopyOwnerToClosest(BarricadeClass barr, float radius = 5f, uint ignoreId = 0)
        {
            CopyOwnerToClosest(barr.BarricadeTransform, radius, ignoreId);
            //List<Transform> res = new List<Transform>();
            //BarricadeManager.getBarricadesInRadius(barr.BarricadeTransform.position, radius, res);
            //BarricadeDrop drop = res.FirstOrDefault()?.parent as BarricadeDrop;
        }
        public static void CopyOwnerToClosest(Transform barr, float radius, uint ignoreId = 0)
        {
            List<BarricadeDrop> res = getBarricadesInRadius(barr, radius, ignoreId); // List<Transform> res = new List<Transform>();
            if (res.Count == 0)
            {
                res = getAbsoluteBarricadesInRadius(barr, radius, ignoreId);
            }
            // BarricadeManager.getBarricadesInRadius(barr.position, radius, res);
            // IEnumerable<Transform> g2 = res.OrderBy(tran => Vector3.Distance(tran.position, barr.position));
            BarricadeDrop barrB = res.FirstOrDefault(gg=>gg.instanceID!=barr.GetInstanceID());
            /*foreach(BarricadeDrop gg in res)
            {
                barrB = gg; // BarricadeManager.FindBarricadeByRootTransform(gg);
                if (barrB.instanceID != barr.GetInstanceID()) break;
            }*/
            if (barrB == null) return;
            BarricadeDrop barrA = BarricadeManager.FindBarricadeByRootTransform(barr);
            BarricadeData g = barrB.GetServersideData();
            BarricadeData b = barrA.GetServersideData();
            barrA.ReceiveOwnerAndGroup(g.owner, g.group);
            b.owner = g.owner; 
            b.group = g.group;
            BarricadeManager.changeOwnerAndGroup(barrA.model, g.owner, g.group);
        }
        [ScriptFunction("SetOwner")]
        public static void SetOwner(BarricadeClass ba, string owner, string group = "")
        {
            BarricadeDrop barr = BarricadeManager.FindBarricadeByRootTransform(ba.BarricadeTransform);
            barr.ReceiveOwnerAndGroup(ulong.Parse(owner), ulong.Parse(group));
        }
        [ScriptFunction("GetOwnerFromClosest")]
        public static string GetOwnerFromClosest(BarricadeClass barr, float radius, uint ignoreId = 0)
        {
            return GetOwnerFromClosest(barr.BarricadeTransform, radius, ignoreId);
        }
        public static string GetOwnerFromClosest(Transform barr, float radius, uint ignoreId = 0)
        {
            List<BarricadeDrop> res = getBarricadesInRadius(barr, radius, ignoreId);
            if (res.Count == 0)
            {
                res = getAbsoluteBarricadesInRadius(barr, radius, ignoreId);
            }
            List<Transform> res2 = new List<Transform>();
            List<RegionCoordinate> search = new List<RegionCoordinate>();
            // BarricadeManager.getBarricadesInRadius(barr.position, radius, res);
            StructureManager.getStructuresInRadius(barr.position, radius, search, res2);
#if DEBUG
            Logger.Log("Found " + res.Count.ToString() + " barricade Transforms near that barricade.");
            Logger.Log("Found " + res2.Count.ToString() + " structure Transforms near that barricade.");
            if(res.Count == 0)
            {
                res = getAbsoluteBarricadesInRadius(barr, radius, ignoreId);
                Logger.Log("Absolute find: " + res.Count.ToString());
            }
#endif
            BarricadeDrop barrB = res.FirstOrDefault(gg=>gg.instanceID!=barr.GetInstanceID());
#if DEBUG
            //Logger.Log("Found "+g2.Count().ToString()+" barricade Transforms near that barricade. (Enumerable)");
#endif
            /*foreach (BarricadeDrop gg in res)
            {
                barrB = gg;
#if DEBUG
                Logger.Log("Checking: " + barrB.instanceID.ToString() + ", compare with: "+barr.GetInstanceID());
#endif
                if (barrB.instanceID != barr.GetInstanceID()) break;
            }*/
            if (barrB == null) return "";
            BarricadeData g = barrB.GetServersideData();
            return g.owner.ToString();
        }
        [ScriptFunction("CopyOwner")]
        public static void CopyOwner(uint instanceId, uint toInstanceId)
        {

        }
        [ScriptFunction("CopyOwnerFromClosestAll")]
        public static void CopyOwnerFromClosestAll(ushort id, float radius = 5f)
        {
            List<BarricadeDrop> gg = FindAll(id);
            foreach(BarricadeDrop g in gg)
            {
                CopyOwnerToClosest(g.model, radius, id);
            }
        }
        [ScriptFunction("FindAllId")]
        public static ExpressionValue FindAllId(ushort id)
        {
            List<BarricadeDrop> _list = FindAll(id);
            IList<ExpressionValue> list2 = new List<ExpressionValue>();
            foreach(var gg in _list)
            {
                var barr = ExpressionValue.CreateObject(new BarricadeClass(gg.model));
                list2.Add(barr);
            }
            return ExpressionValue.Array(list2);
        }
        [ScriptFunction("FindAllId")]
        public static ExpressionValue FindAllId(ushort id, string ownerId)
        {
            List<BarricadeDrop> _list = FindAll(id).Where(gg=>gg.GetServersideData().owner.ToString()==ownerId).ToList();
            IList<ExpressionValue> list2 = new List<ExpressionValue>();
            foreach (var gg in _list)
            {
                var barr = ExpressionValue.CreateObject(new BarricadeClass(gg.model));
                list2.Add(barr);
            }
            return ExpressionValue.Array(list2);
        }
        [ScriptFunction("FindAllIdInRegion")]
        public static ExpressionValue FindAllIdInRegion(BarricadeClass b, ushort id, string ownerId)
        {
            List<BarricadeDrop> _list = FindAllInRegion(b.BarricadeTransform,id).Where(gg => gg.GetServersideData().owner.ToString() == ownerId).ToList();
            IList<ExpressionValue> list2 = new List<ExpressionValue>();
            foreach (var gg in _list)
            {
                var barr = ExpressionValue.CreateObject(new BarricadeClass(gg.model));
                list2.Add(barr);
            }
            return ExpressionValue.Array(list2);
        }
        public static List<BarricadeDrop> FindAll(ushort id)
        {
            var buildables = BarricadeManager.regions.Cast<BarricadeRegion>().Concat(BarricadeManager.vehicleRegions)
                    .SelectMany(k => k.drops)
                    .Where(k => k.asset.id == id).ToList();
            return buildables;
        }
        public static List<BarricadeDrop> FindAllInRegion(Transform barr, ushort id)
        {
            List<BarricadeDrop> drops = new List<BarricadeDrop>();
            if (BarricadeManager.tryGetRegion(barr, out byte x, out byte y, out ushort plant, out BarricadeRegion reg))
            {
                drops = reg.drops.Where(tran => tran.asset.id == id).ToList();
            }
            return drops;
        }
        public static List<BarricadeDrop> getBarricadesInRadius(Transform barr, float rad, uint ignoreId)
        {
            List<BarricadeDrop> drops = new List<BarricadeDrop>();
            if (BarricadeManager.tryGetRegion(barr, out byte x, out byte y, out ushort plant, out BarricadeRegion reg))
            {
                drops = reg.drops.Where(tran => tran.asset.id != ignoreId && Vector3.Distance(tran.model.position, barr.position) < rad).OrderBy(tran => Vector3.Distance(tran.model.position, barr.position)).ToList();
            }

            return drops;
        }
        public static List<BarricadeDrop> getAbsoluteBarricadesInRadius(Transform barr, float rad, uint ignoreId)
        {
            List<BarricadeDrop> drops = BarricadeManager.regions.Cast<BarricadeRegion>().Concat(BarricadeManager.vehicleRegions)
                    .SelectMany(k => k.drops)
                    .Where(tran => tran.asset.id != ignoreId && Vector3.Distance(tran.model.position, barr.position) < rad)
                    .OrderBy(tran => Vector3.Distance(tran.model.position, barr.position)).ToList();

            return drops;
        }
        [ScriptFunction("GetTown")]
        public static string GetClosestLocation(Vector3Class position)
        {
            return GetClosestLocation(position.Vector3).locationName;
        }
        public static LocationDevkitNode GetClosestLocation(Vector3 position)
        {
            return LevelNodes.nodes
                .OfType<LocationDevkitNode>()
                .OrderBy(l => (l.transform.position - position).sqrMagnitude)
                .FirstOrDefault();
        }
    }
}
