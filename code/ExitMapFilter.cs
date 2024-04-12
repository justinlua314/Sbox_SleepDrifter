using Sandbox;
using System;

public sealed class ExitMapFilter : Component {
	[Property] string TargetMap = "Tunnels";

	[Property] float EyeSpeed = 1f;

	[Property] float IntensityTarget = 10f;

	[Property] SoundEvent Static { get; set; }

	[Property] SoundEvent Ambient { get; set; }

	FilmGrain Grain;
	Vignette Eyelids;

	bool Resting = false;

	public void StartExit() {
		Resting = true;

		if ( Static != null ) {
			Sound.Play( Static );
		}

		if ( Ambient != null ) {
			Sound.Play( Ambient );
		}
	}

	protected override void OnStart() {
		Grain = GameObject.Components.Get<FilmGrain>();
		Eyelids = GameObject.Components.Get<Vignette>();
	}

	protected override void OnFixedUpdate() {
		if (Resting) {
			Grain.Intensity = Math.Min(Grain.Intensity + EyeSpeed / 10f, 1f);
			Eyelids.Intensity += EyeSpeed;

			if ( Eyelids.Intensity >= IntensityTarget ) {
				Eyelids.Intensity = IntensityTarget;

				Grain.Intensity = 1f;
				Grain.Response = 0f;

				Sound.StopAll( 0f );
				Scene.LoadFromFile( $"scenes/{TargetMap}.scene" );
			}
		}
	}
}
