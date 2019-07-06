using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace UnityEngine.Tilemaps
{
	[Serializable]
	[CreateAssetMenu(fileName = "New Random Tile", menuName = "Tiles/RRandom Tile")]
	public class RandomAnimatedTile : Tile
	{
		[SerializeField]
		public Tile[] m_Tiles;

		public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
		{
			base.GetTileData(location, tileMap, ref tileData);
			if ((m_Tiles != null) && (m_Tiles.Length > 0))
			{
				long hash = location.x;
				hash = (hash + 0xabcd1234) + (hash << 15);
				hash = (hash + 0x0987efab) ^ (hash >> 11);
				hash ^= location.y;
				hash = (hash + 0x46ac12fd) + (hash << 7);
				hash = (hash + 0xbe9730af) ^ (hash << 11);
				Random.InitState((int)hash);
				m_Tiles[(int) (m_Tiles.Length * Random.value)].GetTileData(location, tileMap, ref tileData);
			}
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(RandomAnimatedTile))]
	public class RandomAnimatedTileEditor : Editor
	{
		private RandomAnimatedTile tile { get { return (target as RandomAnimatedTile); } }

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			int count = EditorGUILayout.DelayedIntField("Number of Tiles", tile.m_Tiles != null ? tile.m_Tiles.Length : 0);
			if (count < 0)
				count = 0;
			if (tile.m_Tiles == null || tile.m_Tiles.Length != count)
			{
				Array.Resize<Tile>(ref tile.m_Tiles, count);
			}

			if (count == 0)
				return;

			EditorGUILayout.LabelField("Place random sprites.");
			EditorGUILayout.Space();

			for (int i = 0; i < count; i++)
			{
				tile.m_Tiles[i] = (Tile) EditorGUILayout.ObjectField("Sprite " + (i+1), tile.m_Tiles[i], typeof(Tile), false, null);
			}		
			if (EditorGUI.EndChangeCheck())
				EditorUtility.SetDirty(tile);
		}
	}
#endif
}
