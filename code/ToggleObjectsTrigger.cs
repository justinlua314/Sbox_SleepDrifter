using Sandbox;

public sealed class ToggleObjectsTrigger : Component, Component.ITriggerListener, IUse {
	[Property] List<GameObject> objects { get; set; }

	[Property] bool OneTime { get; set; } = true;

	[Property] bool OnEnter { get; set; } = false;
	[Property] bool OnUse { get; set; } = false;

	void Toggle() {
		foreach (GameObject target in objects) {
			target.Enabled = !target.Enabled;
		}

		if (OneTime) { Enabled = false; }
	}

	public void OnTriggerEnter( Collider other ) {
		if ( OnEnter && other.Tags.Has( "player" ) ) {
			Toggle();
		}
	}

	public void Use() {
		if (OnUse) { Toggle(); }
	}
}
