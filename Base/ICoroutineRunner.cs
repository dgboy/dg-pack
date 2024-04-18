using System.Collections;
using UnityEngine;

namespace DG_Pack.Base {
    public interface ICoroutineRunner {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine current);
    }
}