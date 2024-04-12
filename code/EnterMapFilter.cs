using Sandbox;

public sealed class EnterMapFilter : Component {
	[Property] float EyeSpeed = 1f;

	[Property] float IntensityTarget = 10f;
	[Property] float WakeTime { get; set; } = 2f;

	[Property] SoundEvent Static { get; set; }

	[Property] SoundEvent Ambient { get; set; }

	FilmGrain Grain;
	Vignette Eyelids;

	bool Waking = true;

	float WakeTimer = 0f;

	protected override void OnStart() {
		if (Static != null) {
			Sound.Play( Static );
		}

		if ( Ambient != null ) {
			Sound.Play( Ambient );
		}

		Grain = GameObject.Components.Get<FilmGrain>();
		Eyelids = GameObject.Components.Get<Vignette>();

		Grain.Intensity = 1f;
		Grain.Response = 0f;

		Eyelids.Intensity = IntensityTarget;
	}

	protected override void OnFixedUpdate() {
		if ( Waking ) {
			WakeTimer += Time.Delta;

			if ( WakeTimer >= WakeTime ) {
				Waking = false;
			}
		} else {
			Eyelids.Intensity -= EyeSpeed;

			if ( Eyelids.Intensity <= 0f ) {
				Eyelids.Intensity = 0f;
				Sound.StopAll(0f);
				
				if ( Ambient != null ) {
					Sound.Play( Ambient );
				}

				Enabled = false;
			} else {
				float Ratio = 1f / Eyelids.Intensity;
				Grain.Intensity = 1f - 1f * Ratio;
				Grain.Response = 0.5f - 0.5f * Ratio;
			}
		}
	}
}
