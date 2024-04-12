using Sandbox;

public sealed class SendoffCloneScript : Component {
	[Property] GameObject RHand { get; set; }
	[Property] GameObject LHand { get; set; }

	float AnimSpeed = 0.2f;

	Vector3 RHandPosTarget, LHandPosTarget;

	Rotation RHandRotTarget, LHandRotTarget;

	public void SendHandTargets( Transform rHand, Transform lHand) {
		if ( RHand == null || LHand == null ) { return; }

		RHandPosTarget = rHand.Position;
		RHandRotTarget = rHand.Rotation;

		LHandPosTarget = lHand.Position;
		LHandRotTarget = lHand.Rotation;
	}

	protected override void OnStart() {
		RHand.Transform.LocalPosition = new Vector3( -31.637f, -1.594f, -8.246f );
		RHand.Transform.LocalRotation = new Angles( 20.356f, 156.292f, 169.477f ).ToRotation();

		LHand.Transform.LocalPosition = new Vector3( -26.989f, 2.083f, 8.984f );
		LHand.Transform.LocalRotation = new Angles( -14.165f, 176.209f, -172.41f ).ToRotation();

		RHandPosTarget = RHand.Transform.LocalPosition;
		RHandRotTarget = RHand.Transform.LocalRotation;

		LHandPosTarget = LHand.Transform.LocalPosition;
		LHandRotTarget = LHand.Transform.LocalRotation;
	}

	protected override void OnFixedUpdate() {
		RHand.Transform.LocalPosition = Vector3.Lerp(
			RHand.Transform.LocalPosition, RHandPosTarget, AnimSpeed
		);

		LHand.Transform.LocalPosition = Vector3.Lerp(
			LHand.Transform.LocalPosition, LHandPosTarget, AnimSpeed
		);

		RHand.Transform.LocalRotation = Rotation.Lerp(
			RHand.Transform.LocalRotation, RHandRotTarget, AnimSpeed
		);

		LHand.Transform.LocalRotation = Rotation.Lerp(
			LHand.Transform.LocalRotation, LHandRotTarget, AnimSpeed
		);
	}
}
