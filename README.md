# uScriptModule
JHuScriptModule adds a few quality-of-life functions for use in uScript.


```
VehicleMan.GetGuid(instanceId); // string "GUID"
VehicleMan.Spawn("Guid", position, angle);
VehicleMan.SpawnLocked("Guid", position, angle, playerOwnerId);
VehicleMan.SpawnLocked(vehicle, pos, angle, player);
VehicleMan.SpawnLockedByInstance(instanceId, position, angle, playerOwnerId);
VehicleMan.SetColor(vehicle, "hexColor");
VehicleMan.SetColor(instanceId, "hexColor");
VehicleMan.CopyColor(copyFromVehicle, copyToVehicle);
VehicleMan.SetRandomColor(vehicle);
VehicleMan.SetRandomColor(instanceId);
VehicleMan.AddPlayer(vehicle, player);
VehicleMan.AddPlayer(instanceId, playerId);
VehicleMan.AddPlayer(instanceId, playerId, seat); // currently broken
VehicleMan.Teleport(vehicle, Position, vector3Rotation);
VehicleMan.Teleport(instanceId, Position, vector3Rotation);
VehicleMan.Teleport(vehicle, Position, angle);
VehicleMan.Teleport(vehicle, player); // teleports vehicle to player position with player.look.rotation
barricades = BarrMan.FindAllId(53135, player.id);
BarrMan.CopyOwnerToClosest(barricade, r, 53135);
PermissionMan.Exists(groupId);
PermissionMan.Create(groupId, firstMemberId);
PermissionMan.HasMember(groupId, playerId);
PermissionMan.MemberIndex(groupId, playerId); // -1 = not a member, 0 = first member
PermissionMan.AddMember(groupId, playerId);
PermissionMan.RemoveMember(groupId, playerId);
```

Example:
```
vehicle = player.look.getVehicle();
spawnedVeh = VehicleMan.SpawnLockedByInstance(vehicle.instanceId, player.position, 1, player.id);
if(spawnedVeh != null) {
  VehicleMan.SetRandomColor(spawnedVeh.instanceId);
  VehicleMan.AddPlayer(spawnedVeh.instanceId, player.id);
}
```
