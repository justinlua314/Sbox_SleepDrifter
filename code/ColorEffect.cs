using Sandbox;

public sealed class ColorEffect : Component {
	[Property] float Strength { get; set; } = 1f;

	ColorAdjustments Color;

	float ColorAdjustment = 0f;

	protected override void OnStart() {
		Color = Scene.Camera.Components.Get<ColorAdjustments>();
	}

	protected override void OnFixedUpdate() {
		Color.HueRotate = ColorAdjustment;

		ColorAdjustment += Strength;
	}
}
