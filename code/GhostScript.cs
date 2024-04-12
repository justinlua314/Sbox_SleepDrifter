using Sandbox;
using System;

public sealed class GhostScript : Component {
	[Property] GameObject Player { get; set; }

	[Property] ExitMapFilter ExitStrategy { get; set; }

	Vector3 Position = Vector3.Zero;
	Vector3 LastPosition = Vector3.Zero;

	bool Chasing = false;
	float Speed = 0.05f;

	int Stage = -1;

	public void AdvanceHaunting() {
		int count = Stage * 2 - 1;

		foreach ( GameObject ghost in GameObject.Children ) {
			if (count <= 0) { break; }

			if ( ghost != null && ghost.Enabled == false ) {
				ghost.Enabled = true;
				count--;
			}
		}

		if (count > 0) {
			Chasing = true;
			ExitStrategy.StartExit();
		}

		Stage++;
	}

	protected override void OnFixedUpdate() {
		if (Player == null) { return; }

		if ( Chasing ) {
			foreach ( GameObject ghost in GameObject.Children ) {
				if ( ghost != null ) {
					ghost.Transform.Position = Vector3.Lerp(ghost.Transform.Position, Player.Transform.Position, Speed);
				}
			}
		} else {
			Position = Player.Transform.Position;

			foreach ( GameObject ghost in GameObject.Children ) {
				if ( ghost != null ) {
					ghost.Transform.Position = ghost.Transform.Position + (Position - LastPosition);
				}
			}

			LastPosition = Position;
		}
	}
}
