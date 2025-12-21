# uScriptModule
JHuScriptModule adds a few quality-of-life functions for use in uScript.


```
VehicleMan.GetGuid(instanceId); // string "GUID"
VehicleMan.Spawn("Guid", position, angle);
VehicleMan.SpawnLocked("Guid", position, angle, playerOwnerId);
VehicleMan.SpawnLockedByInstance(instanceId, position, angle, playerOwnerId);
VehicleMan.SetColor(instanceId, "hexColor");
VehicleMan.SetRandomColor(instanceId);
VehicleMan.AddPlayer(instanceId, playerId);
VehicleMan.AddPlayer(instanceId, playerId, seat);
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
