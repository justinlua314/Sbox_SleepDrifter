using Sandbox;
using System;

public sealed class PathMovement : Component {
	[Property] List<Vector3> Stops_Pos { get; set; } = new();

	[Property] float Move_Speed { get; set; }
	[Property] float Rotate_Speed { get; set; }

	private int Cur_Stop = 0;

	private void NextStop() {
		Cur_Stop++;

		if (Cur_Stop == Stops_Pos.Count) {
			Cur_Stop = 0;
		}
	}

	protected override void OnFixedUpdate() {
		Rotation target = Rotation.LookAt(
			Stops_Pos[Cur_Stop] - Transform.Position
		);

		Angles angle_target = new(0, target.Angles().yaw, 0);


		Transform.Rotation = Rotation.Lerp( Transform.Rotation, angle_target, Rotate_Speed );
		Transform.Position += Transform.World.Forward * Move_Speed;

		float distance = Vector3.DistanceBetween( Transform.Position, Stops_Pos[Cur_Stop] );

		if (distance < 5.0) {
			NextStop();
		}
	}
}
