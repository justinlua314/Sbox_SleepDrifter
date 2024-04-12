using Sandbox;
using System;

public sealed class UseRig : Component, IUse {
	[Property] GameObject Player { get; set; }

	[Property] GameObject Camera { get; set; }

	[Property] SoundEvent UseSound { get; set; }

	[Property] float StaticSpeed { get; set; } = 0.01f;

	[Property] SoundEvent Static { get; set; }

	[Property] SoundEvent Ambient { get; set; }

	FilmGrain Grain { get; set; }

	bool Using = false;

	public void Use() {
		if (Player == null || !Player.Components.Get<PlayerMovement>().Sitting) {
			return;
		}

		if (Using) { return; }

		if ( UseSound != null ) {
			Random rnd = new();

			UseSound.Pitch = rnd.Next( 70, 85 ) / 100f;
			Sound.Play( UseSound );
		}

		Using = true;

		if ( Static != null ) {
			Sound.Play( Static );
		}

		if ( Ambient != null ) {
			Sound.Play( Ambient );
		}
	}

	protected override void OnStart() {
		Grain = Camera.Components.Get<FilmGrain>();
	}

	protected override void OnFixedUpdate() {
		if (!Using) { return; }

		Grain.Intensity = Math.Min(Grain.Intensity + StaticSpeed, 1f);

		if (Grain.Intensity >= 1f) {
			Grain.Response = Math.Min( Grain.Response + StaticSpeed * 2f, 1f );

			if (Grain.Response == 1f) {
				Sound.StopAll( 0f );
				Camera.Components.Get<ExitMapFilter>().StartExit();
			}
		}
	}
}
