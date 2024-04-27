using Sandbox;

public sealed class RenderTargetToggle : Component {
	/// <summary>
	/// List of Game Objects that will be disabled when c key is pressed
	/// </summary>
	[Property] List<GameObject> RenderTargets { get; set; }

	protected override void OnFixedUpdate() {
		if (Input.Pressed("View")) {
			if (RenderTargets != null) {
				foreach (GameObject target in RenderTargets) {
					target.Enabled = false;
				}
			}

			GameObject.Enabled = false;
		}
	}
}
