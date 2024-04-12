using Sandbox;

public sealed class LookHaunt : Component, ILook {
	[Property] LookHaunt Opposite { get; set; }

	[Property] GhostScript Control { get; set; }

	public bool Locked = false;

	public void StartLook() {
		if (Locked) { return; }

		Control.AdvanceHaunting();
		Locked = true;

		if (Opposite != null) {
			Opposite.Locked = false;
		}
	}

	public void StopLook() {
	}
}
