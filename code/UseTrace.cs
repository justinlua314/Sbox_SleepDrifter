using Sandbox;

public sealed class UseTrace : Component {
	[Property] float ReachDistance { get; set; } = 80f;

	List<GameObject> LookingAt = new();
	List<GameObject> LookBuffer = new();

	protected override void OnFixedUpdate() {
		if (GameObject.Parent == null) { return; }

		Angles direction = Transform.Rotation.Angles();

		Vector3 start = Transform.Position + (Vector3.Forward * direction * 5f);
		Vector3 end = start + (Vector3.Forward * direction * ReachDistance);

		var eyetrace = Scene.Trace.Ray(
			start, end
		).IgnoreGameObject(GameObject.Parent);

		// Run trace from eyes
		var results = eyetrace.RunAll();

		LookBuffer.Clear();

		// For everything hit by the trace
		foreach (SceneTraceResult scan in results) {
			// If results contains IUse and we click it, Use
			var use_func = scan.Component.Components.Get<IUse>();

			if ( use_func != null ) {
				if ( Input.Pressed( "attack1" ) || Input.Pressed("use") || Input.Pressed("flashlight") ) {
					use_func.Use();
				}
			}

			// If result contains new ILook component, StartLook
			var look = scan.Component.Components.Get<ILook>();

			if (look != null) {
				if (!LookingAt.Contains(scan.GameObject)) {
					look.StartLook();
					LookingAt.Add(scan.GameObject);

					var glow = scan.Component.Components.Get<HighlightOutline>(true);

					if (glow != null && use_func != null) {
						glow.Enabled = true;
					}
				}
				
				LookBuffer.Add(scan.GameObject);
			}
		}

		// Anything that we were looking at before that we don't see now, call StopLook
		List<int> garbage = new();
		int index = 0;

		foreach(GameObject look in LookingAt) {
			if (look == null) { continue; }

			if (look.Enabled == false) {
				garbage.Add(index);
				index++;
				continue;
			}

			if (!LookBuffer.Contains(look)) {
				var look_comp = look.Components.Get<ILook>();

				if ( look_comp != null ) {
					look_comp.StopLook();
				}

				var glow = look.Components.Get<HighlightOutline>();

				if ( glow != null ) {
					glow.Enabled = false;
				}

				garbage.Add( index );
			}

			index++;
		}

		index = garbage.Count - 1;

		while (index >= 0 ) {
			LookingAt.RemoveAt( index );
			index--;
		}
	}
}
