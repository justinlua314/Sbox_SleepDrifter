using Sandbox;

public sealed class SendoffAnim : Component {
	[Property] List<GameObject> GroupOne { get; set; }
	[Property] List<GameObject> GroupTwo { get; set; }


	[Property] List<Transform> RTransforms { get; set; }
	[Property] List<Transform> LTransforms { get; set; }

	[Property] DeathScript Death { get; set; }

	const float TotalTimer = 0.75f;
	float Timer = TotalTimer;

	bool blink = true;

	int count = 5;

	protected override void OnFixedUpdate() {
		if (count == 0) { Death.Pitching = true; Enabled = false; return; }

		Timer -= Time.Delta;

		if (Timer <= 0) {
			Timer += TotalTimer;

			foreach ( GameObject target in GroupOne ) {
				SendoffCloneScript script = target.Components.Get<SendoffCloneScript>();

				if ( blink ) {
					script.SendHandTargets( RTransforms[0], LTransforms[0] );
				} else {
					script.SendHandTargets( RTransforms[1], LTransforms[1] );
				}
			}

			foreach ( GameObject target in GroupTwo ) {
				SendoffCloneScript script = target.Components.Get<SendoffCloneScript>();

				if ( blink ) {
					script.SendHandTargets( RTransforms[1], LTransforms[1] );
				} else {
					script.SendHandTargets( RTransforms[0], LTransforms[0] );
				}
			}

			blink = !blink;
			count--;
		}
	}
}
