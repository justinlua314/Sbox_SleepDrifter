using Sandbox;
using System;

public sealed class TripTrigger : Component, Component.ITriggerListener {
	[Property] GameObject Camera { get; set; }

	[Property] SoundEvent Song { get; set; }

	Pixelate Pix;
	ChromaticAberration Chrome;
	FilmGrain Grain;

	float PixelSpeed = 0.01f;
	float ChromeSpeed = 0.005f;

	bool Tripping = false;
	bool PixelDirection = true;
	bool Transitioning = false;

	Random Rnd = new();

	protected override void OnStart() {
		Pix = Camera.Components.Get<Pixelate>();
		Chrome = Camera.Components.Get<ChromaticAberration>();
		Grain = Camera.Components.Get<FilmGrain>();
	}

	public void OnTriggerEnter( Collider other ) {
		if (other.Tags.Has("player")) {
			Tripping = true;

			if (Song != null) {
				Sound.Play(Song);
			}
		}
	}

	public void OnTriggerExit( Collider other ) {
	}

	protected override void OnFixedUpdate() {
		if (!Tripping) { return; }

		if ( PixelDirection ) {
			Pix.Scale += PixelSpeed;

			if ( Pix.Scale >= 1f ) { PixelDirection = false; }
		} else {
			Pix.Scale -= PixelSpeed;

			if ( Pix.Scale < 0f ) { PixelDirection = true; }
		}

		Chrome.Scale = Math.Min( Chrome.Scale + ChromeSpeed, 1f );

		Vector3 offset = Chrome.Offset;

		offset.x += Rnd.Next( 0, 3 );
		offset.y += Rnd.Next( 0, 3 );
		offset.z += Rnd.Next( 0, 3 );

		Chrome.Offset = offset;

		if ( offset.x + offset.y + offset.z >= 3000f ) {
			Grain.Intensity += 0.1f;

			if ( Grain.Intensity >= 1f && !Transitioning ) {
				Camera.Components.Get<ExitMapFilter>().StartExit();
				Transitioning = true;
			}
		}
	}
}
