using Sandbox;
using System;

public sealed class BananaTrigger : Component, Component.ITriggerListener {
	public void OnTriggerEnter( Collider other ) {
		var player = other.Components.Get<PlayerMovement>();

		Log.Info( "Collision" );

		if ( player != null ) {
			Random rnd = new();
			this.GameObject.Components.Get<Rigidbody>().Velocity = new Vector3(
				rnd.Next(-500, 500),
				rnd.Next(-500, 500),
				rnd.Next(500, 2500)
			);
		}
	}

	public void OnTriggerExit( Collider other ) {

	}
}
