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
VehicleMan.SetRandomColor(vehicle);
VehicleMan.SetRandomColor(instanceId);
VehicleMan.AddPlayer(vehicle, player);
VehicleMan.AddPlayer(instanceId, playerId);
VehicleMan.AddPlayer(instanceId, playerId, seat); // currently broken
VehicleMan.Teleport(vehicle, Position, vector3Rotation);
VehicleMan.Teleport(instanceId, Position, vector3Rotation);
VehicleMan.Teleport(vehicle, player); // teleports vehicle to player position with player.look.rotation
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
