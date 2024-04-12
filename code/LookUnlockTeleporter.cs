using Sandbox;

public sealed class LookUnlockTeleporter : Component, ILook {
	[Property] Teleporter Portal { get; set; }

	public void StartLook() {
		if ( Portal != null ) {
			Portal.P_Enabled = true;
			Portal.EjectPlayer();
		}
	}

	public void StopLook() {
		if ( Portal != null ) {
			Portal.P_Enabled = false;
		}
	}
}
