using UnityEngine;
using UnityEngine.AI;

public class MoveToClickPoint : MonoBehaviour {
    [SerializeField] private NavMeshAgent agent;

    //void Start() {
    //    agent = GetComponent<NavMeshAgent>();
    //}

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100)) {
                agent.destination = hit.point;
            }
        }
    }
}