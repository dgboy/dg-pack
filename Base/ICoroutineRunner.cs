using System.Collections;
using UnityEngine;

namespace Core.Base {
    public interface ICoroutineRunner {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}