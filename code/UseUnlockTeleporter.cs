using Sandbox;
using System;

public sealed class UseUnlockTeleporter : Component, IUse {
	[Property] Teleporter Portal { get; set; }

	[Property] SoundEvent UseSound { get; set; }

	public void Use() {
		if (Portal != null) {
			if (UseSound != null) {
				Random rnd = new();

				UseSound.Pitch = rnd.Next( 70, 85 ) / 100f;
				Sound.Play(UseSound);
			}

			Portal.P_Enabled = true;
			GameObject.Enabled = false;
		}
	}
}
