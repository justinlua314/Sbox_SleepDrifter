using Sandbox;

public sealed class UseCouch : Component, IUse {
	[Property] GameObject Player { get; set; }

	[Property] GameObject Rig { get; set; }

	[Property] GameObject Clone { get; set; }

	[Property] GameObject CloneLight { get; set; }

	bool Sitting = false;

	public void Use() {
		if ( Player != null ) {
			Sitting = true;
			Clone.Enabled = true;
			CloneLight.Enabled = true;

			var skin = Player.Components.Get<SkinnedModelRenderer>();
			AnimationGraph anim = AnimationGraph.Load("animgraphs/clone_sit");

			skin.AnimationGraph = anim;

			ModelCollider collider = GameObject.Components.Get<ModelCollider>();

			if ( collider != null ) {
				collider.Enabled = false;
			}

			// Player.Components.Get<CapsuleCollider>().Enabled = false;
			Player.Transform.Position = new Vector3( 35f, -369f, 190f );

			Angles window_target = new( 0, -178, 0 );
			PlayerMovement plyMove = Player.Components.Get<PlayerMovement>();

			Player.Transform.ClearLerp();
			Player.Transform.Rotation = window_target.ToRotation();

			plyMove.WishVelocity = Vector3.Zero;
			plyMove.MoveFrozen = true;
			plyMove.Sitting = true;
			plyMove.EyeAngles = window_target;

			Rig.Components.Get<LookTriggerExample>( true ).Enabled = true;
		}
	}

	protected override void OnFixedUpdate() {
		if (Sitting) {
			Player.Transform.Position = new Vector3( 35f, -369f, 190f );
		}
	}
}
