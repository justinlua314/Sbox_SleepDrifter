using Sandbox;
using System;

public sealed class Teleporter : Component, Component.ITriggerListener {
	[Property] Vector3 TeleportDirection { get; set; }

	[Property] public bool P_Enabled { get; set; }

	bool PlayerInside = false;

	public void OnTriggerEnter( Collider other ) {
		if ( other.Tags.Has( "player" ) ) {
			PlayerInside = true;

			if ( P_Enabled) {
				other.Transform.Position += TeleportDirection;
				other.Transform.ClearLerp();
			}
		}
	}

	public void OnTriggerExit( Collider other ) {
		if ( other.Tags.Has( "player" ) ) {
			PlayerInside = false;
		}
	}

	public void EjectPlayer() {
		if ( !PlayerInside ) { return; }

		foreach (GameObject target in Scene.GetAllObjects(true)) {
			if (target.Tags.Has("player")) {
				target.Transform.Position += TeleportDirection;
				target.Transform.ClearLerp();
				break;
			}
		}
	}
}
