using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider2D) )]
[RequireComponent (typeof (SpriteRenderer) )]
public class WorldInteractable : Interactable {

	protected Collider2D cldr;
	//protected SpriteRenderer spriteRenderer;
	protected Renderer renderer;
	public Sprite groundSprite;
	
	override protected void Start () {
		base.Start ();
		cldr = GetComponent <Collider2D> ();
		renderer = GetComponent <Renderer> ();
		if (groundSprite == null) {
			throw new UnityException ("No ground sprite set");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
