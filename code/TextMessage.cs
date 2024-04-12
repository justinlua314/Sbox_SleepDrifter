using Sandbox;
using System;

public sealed class TextMessage : Component, IUse {
	[Property] GameObject Text { get; set; }

	[Property] float MessageTime { get; set; }

	[Property] SoundEvent UseSound { get; set; }

	float Timer = 0f;
	bool displayed = false;

	public void Use() {
		if (Text == null || displayed) { return; }

		Text.Enabled = true;
		displayed = true;

		if ( UseSound != null ) {
			Random rnd = new();

			UseSound.Pitch = rnd.Next( 70, 85 ) / 100f;
			Sound.Play( UseSound );
		}
	}

	protected override void OnFixedUpdate() {
		if (!displayed) { return; }

		Timer += Time.Delta;

		if (Timer >= MessageTime) {
			Timer = 0f;
			Text.Enabled = false;
			displayed= false;
		}
	}
}
