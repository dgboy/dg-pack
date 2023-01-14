using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace InGameSchedule {
    public class InGameTime : MonoBehaviour {
        public static Action<DateTime> OnYearChanged;
        public static Action<DateTime> OnMonthChanged;
        public static Action<DateTime> OnDayChanged;
        public static Action<DateTime> OnHourChanged;
        public static Action<DateTime> OnMinuteChanged;


        [Range(0.01f, 2.00f)] [SerializeField] private float timeScale = 0.5f;
        [SerializeField] private string startDate = "01.09.980";
        [SerializeField] private string startTime = "06:00";

        private DateTime _now;
        private float _seconds;
        private float totalDays;

        public DateTime Now { get => _now; }

        private void Awake() {
            // TODO: if not load, init default
            _now = DateTime.ParseExact($"{startDate} {startTime}", "dd.MM.yyy HH:mm", CultureInfo.InvariantCulture);

            //DontDestroyOnLoad(this.gameObject);
        }

        private void Update() {
            _seconds += Time.deltaTime;

            if (_seconds >= timeScale) {
                _now = Now.AddMinutes(1);
                _seconds = 0;

                OnMinuteChanged?.Invoke(Now);
                if (Now.Minute == 0) {
                    OnHourChanged?.Invoke(Now);
                    if (Now.Hour == 0) {
                        Debug.Log("OnDayChanged!");
                        OnDayChanged?.Invoke(Now);
                        if (Now.Day == 1) {
                            Debug.Log("OnMonthChanged!");
                            OnMonthChanged?.Invoke(Now);
                            if (Now.Month == 1) {
                                OnYearChanged?.Invoke(Now);
                            }
                        }
                    }
                }

                //Debug.Log(this);
            }
        }

        public override string ToString() => $"[Day {totalDays:00}] Now: {_now:dd.MM.yyy HH:mm}";
    }
}