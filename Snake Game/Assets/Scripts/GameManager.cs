using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {


	public class GameManager : MonoBehaviour {

		public int maxHeight = 15;
		public int maxWidth = 17;

		public Color color1;
		public Color color2;
		public Color playerColor = Color.black;

		public Transform cameraHolder;

		GameObject playerObject;
		Node playerNode;

		GameObject mapObject;
		SpriteRenderer mapRenderer;

		Node[,] grid;

		bool up, left, right, down;
		bool shouldMovePlayer;
		Direction curDirection;
		public enum Direction {
			up, down, left, right
		}

		#region Init
		public void Start() {

			CreateMap();
			PlacePlayer();
			PlaceCamera();

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

			Sprite sprite = Sprite.Create(txt, rect, Vector2.zero, 1, 0, SpriteMeshType.FullRect);
			mapRenderer.sprite = sprite;

		}

		void PlacePlayer() {

			playerObject = new GameObject("Player");
			SpriteRenderer playerRenderer = playerObject.AddComponent<SpriteRenderer>();
			playerRenderer.sprite = CreateSprite(playerColor);
			playerRenderer.sortingOrder = 1;

			playerNode = GetNode(3, 3);
			playerObject.transform.position = playerNode.worldPosition;

		}

		void PlaceCamera() {
			Node n = GetNode(maxWidth / 2, maxHeight / 2);
			Vector3 p = n.worldPosition;
			p += Vector3.one * .5f;
			cameraHolder.position = p;
		}

		#endregion

		#region Update

		private void Update() {
			GetInput();
			SetPlayerDirection();
			MovePlayer();
		}

		void GetInput() {

			up = Input.GetButtonDown("Up");
			down = Input.GetButtonDown("Down");
			left = Input.GetButtonDown("Left");
			right = Input.GetButtonDown("Right");

		}

		void SetPlayerDirection() {
			if (up) {
				curDirection = Direction.up;
				shouldMovePlayer = true;
			} else if (down) {
				curDirection = Direction.down;
				shouldMovePlayer = true;
			} else if (left) {
				curDirection = Direction.left;
				shouldMovePlayer = true;
			} else if (right) {
				curDirection = Direction.right;
				shouldMovePlayer = true;
			}
		}

		void MovePlayer() {

			if (!shouldMovePlayer) return;

			shouldMovePlayer = false;

			int x = 0;
			int y = 0;

			switch (curDirection) {
				case Direction.up:
					y = 1;
					break;
				case Direction.down:
					y = -1;
					break;
				case Direction.left:
					x = -1;
					break;
				case Direction.right:
					x = 1;
					break;
			}

			Node targetNode = GetNode(playerNode.x + x, playerNode.y + y);
			if (targetNode == null) {
				// Game over
			} else {
				playerObject.transform.position = targetNode.worldPosition;
				playerNode = targetNode;
			}
		}

		#endregion

		#region Utililities
		Node GetNode(int x, int y) {

			if (x < 0 || x > maxWidth - 1 || y < 0 || y > maxHeight - 1) {
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
			return Sprite.Create(txt, rect, Vector2.zero, 1, 0, SpriteMeshType.FullRect);
		}
		#endregion

	}
}
