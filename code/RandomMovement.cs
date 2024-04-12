using Sandbox;
using System;

public sealed class RandomMovement : Component {
	[Property] bool PunchX {get; set;}
	[Property] bool PunchY { get; set; }
	[Property] bool PunchZ { get; set; }

	[Property] int MinForce { get; set; }
	[Property] int MaxForce { get; set; }

	[Property] int MinTime { get; set; }
	[Property] int MaxTime { get; set; }

	private Random Rand = new();

	private bool Cooldown = false;

	private async void DoPunch() {
		Vector3 punch = new() {
			x = PunchX ? Rand.Next( MinForce, MaxForce ) : 0,
			y = PunchY ? Rand.Next( MinForce, MaxForce ) : 0,
			z = PunchZ ? Rand.Next( MinForce, MaxForce ) : 0
		};

		GameObject.Components.Get<Rigidbody>().Velocity = punch;

		await Task.Delay( Rand.Next( MinTime, MaxTime ) );
		Cooldown = false;
	}

	protected override void OnUpdate() {
		if (Cooldown) { return; }

		Cooldown = true;
		DoPunch();
	}
}
