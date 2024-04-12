using Sandbox;

public sealed class PlatformShrink : Component {
	[Property] List<GameObject> Messages { get; set; }

	[Property] float ShrinkSpeed { get; set; } = 1f;

	BoxCollider Collision;

	bool Shrinking = true;
	bool MessagesShown = false;

	bool Zero(Vector3 scale) {
		return (scale.x <= 0 || scale.y <= 0 || scale.z <= 0);
	}

	protected override void OnStart() {
		Collision = GameObject.Components.Get<BoxCollider>();
	}

	protected override void OnFixedUpdate() {
		if (!Shrinking) { return; }

		Vector3 curScale = GameObject.Transform.Scale;
		Vector3 newScale = new(curScale.x - ShrinkSpeed, curScale.y - ShrinkSpeed, curScale.z);

		GameObject.Transform.Scale = newScale;
		Collision.Scale -= ShrinkSpeed;

		if ( !MessagesShown ) {
			float size = newScale.x + newScale.y + newScale.z;

			if (size < 18.75f) {
				foreach (GameObject message in Messages) {
					message.Enabled = true;
				}

				MessagesShown = true;
			}
		}

		if (Zero(GameObject.Transform.Scale)) {
			GameObject.Transform.Scale = Vector3.Zero;
			Shrinking = false;
			GameObject.Enabled = false;
		}
	}
}
