using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour {
    public FloatValue PlayerHealth { get; set; }
    public Notification healhSignal;

    public void Use(int amountToIncrease) {
        PlayerHealth.value += amountToIncrease;
        healhSignal.Raise();
    }
}
