using Sandbox;

public sealed class TeleporterActivator : Component, Component.ITriggerListener {
	[Property] Teleporter Target { get; set; }

	public void OnTriggerEnter( Collider other ) {
		if (other.Tags.Has("player")) {
			Target.P_Enabled = true;
			Enabled = false;
		}
	}

	public void OnTriggerExit( Collider other ) {

	}
}
