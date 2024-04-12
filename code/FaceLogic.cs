using Sandbox;

public sealed class FaceLogic : Component {
	[Property] GameObject LookTarget { get; set; }

	[Property] Angles Offset { get; set; }

	[Property] float Speed { get; set; } = 0.05f;

	[Property] float Range { get; set; } = 500f;

	protected override void OnFixedUpdate() {
		if (LookTarget != null) {
			Vector3 target = LookTarget.Transform.Position;

			if (Vector3.DistanceBetween(Transform.Position, target) > Range ) {
				return;
			}

			Rotation target_rot = Rotation.LookAt(target - Transform.Position);

			Angles target_ang = target_rot.Angles() + Offset;

			target_ang.pitch = 0f;

			Transform.Rotation = Rotation.Lerp(
				Transform.Rotation, target_ang, Speed
			);
		}
	}
}
