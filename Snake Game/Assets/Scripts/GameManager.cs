﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {


	public class GameManager : MonoBehaviour {

		public int maxHeight = 15;
		public int maxWidth = 17;

		public Color color1;
		public Color color2;
		public Color playerColor = Color.black;

		GameObject playerObject;

		GameObject mapObject;
		SpriteRenderer mapRenderer;

		Node[,] grid;

		public void Start() {

			CreateMap();
			PlacePlayer();

		}

		void CreateMap() {

			mapObject = new GameObject("Map");
			mapRenderer = mapObject.AddComponent<SpriteRenderer>();

			grid = new Node[maxWidth, maxHeight];

			Texture2D txt = new Texture2D(maxWidth, maxHeight);
			for (int x = 0; x < maxWidth; x++) {
				for (int y = 0; y < maxHeight; y++) {

					Vector3 tp = Vector3.zero;
					tp.x = x;
					tp.y = y;

					Node n = new Node() {
						x = x,
						y = y,
						worldPosition = tp
					};

					grid[x, y] = n;

					#region CreateTexture
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
					#endregion

				}
			}

			txt.filterMode = FilterMode.Point;
			txt.Apply();

			Rect rect = new Rect(0, 0, maxWidth, maxHeight);

			Sprite sprite = Sprite.Create(txt, rect, Vector2.one * .5f, 1, 0, SpriteMeshType.FullRect);
			mapRenderer.sprite = sprite;

		}

		void PlacePlayer() {

			playerObject = new GameObject("Player");
			SpriteRenderer playerRenderer = playerObject.AddComponent<SpriteRenderer>();
			playerRenderer.sprite = CreateSprite(playerColor);
			playerRenderer.sortingOrder = 1;

			playerObject.transform.position = GetNode(3, 3).worldPosition;

		}

		Node GetNode(int x, int y) {

			if (x < 0 || x > maxWidth - 1 || y < 0 || y > maxHeight - 1){
				return null;
			}

			return grid[x, y];
		}

		Sprite CreateSprite(Color targetColor) {
			Texture2D txt = new Texture2D(1, 1);
			txt.SetPixel(0, 0, targetColor);
			txt.Apply();
			txt.filterMode = FilterMode.Point;

			Rect rect = new Rect(0, 0, 1, 1);
			return Sprite.Create(txt, rect, Vector2.one * .5f, 1, 0, SpriteMeshType.FullRect);
		}

	}
}
