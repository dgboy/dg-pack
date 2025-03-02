using UnityEngine;

namespace DG_Pack.Pathfinding {
    public class PathfinderHub : MonoBehaviour {
        public GridManagerBase grid;
        public AStar pathfinder;
        public Agent agent;
        // public PathVisualizer pathVisualizer;

        public void Awake() {
            pathfinder = new AStar(grid);
            grid.Initialize();
        }
    }
}