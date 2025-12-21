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
