using Sandbox;

public sealed class RenderTarget : Component {
	[Property] public CameraComponent camera { get; set; }

	public ModelRenderer model;

	private Texture tex { get; set; }

	protected override void OnStart() {
		if (camera == null) {
			camera = Game.ActiveScene.Camera;
		}

		model = Components.Get<ModelRenderer>();

		tex = Texture.CreateRenderTarget()
			.WithSize( camera.ScreenRect.Width.CeilToInt(), camera.ScreenRect.Height.CeilToInt() )
			.Create();
	}

	protected override void OnFixedUpdate() {
		camera.RenderToTexture( tex );

		model.SceneObject.Attributes.Set( "render_target", tex );
	}
}
