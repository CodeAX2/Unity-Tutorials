using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {


	public class GameManager : MonoBehaviour {

		public int maxHeight = 15;
		public int maxWidth = 17;

		public Color color1;
		public Color color2;

		GameObject mapObject;
		SpriteRenderer mapRenderer;

		public void Start() {

			CreateMap();

		}

		void CreateMap() {

			mapObject = new GameObject("Map");
			mapRenderer = mapObject.AddComponent<SpriteRenderer>();

			Texture2D txt = new Texture2D(maxWidth, maxHeight);
			for (int x = 0; x < maxWidth; x++) {
				for (int y = 0; y < maxHeight; y++) {

					if (x % 2 != 0) {
						if (y % 2 != 0) {
							txt.SetPixel(x, y, color1);
						} else {
							txt.SetPixel(x, y, color2);
						}
					} else {
						if (y % 2 != 0) {
							txt.SetPixel(x, y, color2);
						} else {
							txt.SetPixel(x, y, color1);
						}
					}

				}
			}

			txt.filterMode = FilterMode.Point;
			txt.Apply();

			Rect rect = new Rect(0, 0, maxWidth, maxHeight);

			Sprite sprite = Sprite.Create(txt, rect, Vector2.one * .5f, 1, 0, SpriteMeshType.FullRect);
			mapRenderer.sprite = sprite;

		}

	}
}
