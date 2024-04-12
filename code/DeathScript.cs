using Sandbox;
using System;

public sealed class DeathScript : Component, IUse {
	[Property] GameObject Player { get; set; }

	[Property] GameObject SendOff { get; set; }

	[Property] SoundEvent Song { get; set; }

	[Property] SoundEvent UseSound { get; set; }

	Angles PlyAng;

	ChromaticAberration Chrome;
	FilmGrain Grain;

	bool Dying = false;
	bool Waiting = true;
	public bool Pitching = false;
	bool Falling = false;

	int WaitCounter = 30;
	float ChromaticTimer = 3f;
	int ChromeSlowdown = 150;

	Random Rnd = new();

	bool dead = false;

	public void Use() {
		if (Dying || dead) { return; }

		if ( UseSound != null ) {
			Random rnd = new();

			UseSound.Pitch = rnd.Next( 70, 85 ) / 100f;
			Sound.Play( UseSound );
		}

		PlayerMovement move = Player.Components.Get<PlayerMovement>();

		move.MoveFrozen = true;
		move.LookFrozen = true;

		Dying = true;

		PlyAng = Player.Transform.Rotation.Angles();

		Chrome = Scene.Camera.Components.Get<ChromaticAberration>(true);
		Chrome.Enabled = true;

		Grain = Scene.Camera.Components.Get<FilmGrain>();
	}

	protected override void OnFixedUpdate() {
		if (!Dying) { return; }

		if (Waiting) {
			SendOff.Transform.LocalPosition += Vector3.Left * 6f;
			WaitCounter--;

			if (WaitCounter == 0) {
				Waiting = false;
				SendOff.Components.Get<SendoffAnim>(true).Enabled = true;
			}
		}

		if ( Pitching ) {
			Angles target = new( PlyAng.pitch - 3f, PlyAng.yaw, PlyAng.roll );

			Player.Transform.Rotation = target.ToRotation();

			PlyAng = Player.Transform.Rotation.Angles();

			if (PlyAng.pitch <= -90f) {
				Pitching = false;
				Falling = true;

				if ( Song != null ) {
					Sound.Play( Song );

					Log.Info( "Playing Song" );
				}
			}
		}

		if (Falling && !dead) {
			Player.Transform.Position += Vector3.Down * 25f;
			Scene.Camera.FieldOfView = Math.Min(Scene.Camera.FieldOfView + 0.3f, 150f);

			if (ChromaticTimer > 0) {
				ChromaticTimer = Math.Min( ChromaticTimer - Time.Delta, 0 );

				if (ChromaticTimer == 0) {
					
				}
			} else {
				Chrome.Scale = Math.Min( Chrome.Scale + 0.1f, 1f );

				if ( ChromeSlowdown > 0 ) {
					ChromeSlowdown--;
				} else {
					Vector3 offset = Chrome.Offset;

					offset.x += Rnd.Next( 1, 9 );
					offset.y += Rnd.Next( 1, 9 );
					offset.z += Rnd.Next( 1, 9 );

					Chrome.Offset = offset;

					if (offset.x + offset.y + offset.z >= 3500f) {
						Grain.Intensity += 0.01f;

						if ( Grain.Intensity >= 1f ) {
							Scene.Camera.Components.Get<ExitMapFilter>().StartExit();
							dead = true;
						}
					}
				}
			}
		}
	}
}
