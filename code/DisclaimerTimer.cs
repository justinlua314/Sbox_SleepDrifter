using Sandbox;

public sealed class DisclaimerTimer : Component {
	[Property] float Timer { get; set; } = 3f;

	float time = 0f;

	protected override void OnFixedUpdate() {
		time += Time.Delta;

		if (time >= Timer) {
			Scene.LoadFromFile( "scenes/MainHub.scene" );
		}
	}
}
