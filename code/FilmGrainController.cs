using Sandbox;
using System;

public sealed class FilmGrainController : Component {
	[Property] GameObject FocusObject { get; set; }

	[Property] float FocusRadius { get; set; } = 100f;

	[Property] float FocusTime { get; set; } = 5f;

	[Property] SoundEvent Static { get; set; }

	FilmGrain Grain { get; set; }

	private bool Transitioning = false, Transitioned = false;
	private float FTimer = 0f;

	private Vector3 MyPos, TargetPos;
	private float Dist, Ratio;

	protected override void OnStart() {
		Grain = GameObject.Components.Get<FilmGrain>();
	}

	protected override void OnFixedUpdate() {
		if (FocusObject == null || Grain == null ) { return; }

		MyPos = GameObject.Transform.Position;
		TargetPos = FocusObject.Transform.Position;

		Dist = Vector3.DistanceBetween(MyPos, TargetPos);

		if ( Dist <= FocusRadius && !Transitioned ) {
			if ( !Transitioning ) {
				Transitioning = true;

				if ( Static != null ) {
					Sound.Play( Static );
				}
			}

			Ratio = (1f - Dist / FocusRadius);

			Grain.Intensity = Math.Min( Ratio, 1f );
			Grain.Response = Math.Min( (Ratio - 0.8f) * 5f, 1f );
		}

		if (Transitioning && !Transitioned) {
			FTimer += Time.Delta;

			if (FTimer >= FocusTime) {
				GameObject.Components.Get<ExitMapFilter>().StartExit();
				Transitioned = true;
			}
		}
	}
}
