using UnityEngine;
using UnityEngine.UI;

public class UIParticle : MaskableGraphic
{
	[SerializeField] ParticleSystemRenderer particleSystemRenderer;
	[SerializeField] Texture texture;

	public Camera bakeCamera;
	public override Texture mainTexture => texture ?? base.mainTexture;

	bool IsParticleOn;
	float timeOfParticles;

	void Update()
	{
		SetVerticesDirty();
	}

	protected override void OnPopulateMesh(Mesh mesh)
	{
		mesh.Clear();
		if (particleSystemRenderer != null && bakeCamera != null)
		{
			particleSystemRenderer.BakeMesh(mesh, bakeCamera);
		}
	}

}
