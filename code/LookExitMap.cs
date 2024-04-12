using Sandbox;

public sealed class LookExitMap : Component, ILook {
	[Property] ExitMapFilter ExitStrategy { get; set; }

	[Property] FilmGrainController Grain { get; set; }

	public void StartLook() {
		if ( ExitStrategy == null || Grain == null ) { return; }

		Grain.Enabled = true;
		ExitStrategy.StartExit();
	}

	public void StopLook() {
	}
}
