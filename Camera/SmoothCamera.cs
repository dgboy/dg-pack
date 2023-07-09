using System.Collections;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0.1f;

    public Vector2 minPos;
    public Vector2 maxPos;

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    void LateUpdate() {
        if(transform.position != target.position) {
            Vector3 targetPos = new Vector3(
                                target.position.x,
                                target.position.y,
                                transform.position.z);

            //targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            //targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(
                                transform.position, 
                                targetPos, 
                                smoothing);
        }
    }

    public void BeginKick() {
        animator.SetBool("stagger", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo() {
        yield return null;
        animator.SetBool("stagger", false);
    }
}
