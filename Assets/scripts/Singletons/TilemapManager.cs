using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour {
	public GameObject playerHazard;
	
	// Use this for initialization
	void Start () {
		Tilemap map = GetComponent<Tilemap>();
		
		for(int x = map.cellBounds.xMin; x < map.cellBounds.xMax; x++) {
			for(int y = map.cellBounds.yMin; y < map.cellBounds.yMax; y++) {
				Vector3Int tileLocation = new Vector3Int(x, y, (int) map.transform.position.y);
				TileBase mapTile = map.GetTile(tileLocation);
				if(mapTile != null && mapTile.name == "spike") {
					Instantiate(playerHazard, map.CellToWorld(tileLocation), Quaternion.identity);
				}
			}
		}
	}
}
