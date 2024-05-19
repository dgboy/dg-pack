using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.UI.TabMenu.Base {
    public class ItemMenu : MonoBehaviour, ITabMenu {
        public event Action<int, bool> OnSelect;
        public const int DeselectIndex = -1;

        [Header("Settings")]
        [SerializeField] protected bool allowSwitchOff = true;
        [SerializeField] protected bool closePrevious;
        [SerializeField] protected int index = DeselectIndex;

        public List<ITab> Items { get; private set; } = new();
        private bool HasSelected => index != DeselectIndex;

        public int Index {
            get => allowSwitchOff && index < 0 ? index : index % Items.Count;
            set {
                index = allowSwitchOff && value < 0 ? value : value % Items.Count;
                Refresh();
            }
        }


        // private void Start() => Init(null);
        private void OnEnable() => Refresh();

        public void Init(Action<int, bool> callback) {
            OnSelect = callback;
            if (Items.Count == 0) FindItems();
            if (!allowSwitchOff && !HasSelected) Preselect();
        }
        public void Add(ITab tab) {
            if (Items.Contains(tab)) return;

            tab.SetGroup(this);
            Items.Add(tab);
        }
        public void Remove(ITab tab) {
            tab.SetGroup(null);
            Items.Remove(tab);
        }

        public void Select(ITab tab, bool doAction = true) => Select(GetIndexOf(tab), doAction);
        public void Select(int idx, bool doActions = true) {
            bool deselection = allowSwitchOff && Index == idx;

            if (closePrevious && !deselection && Index != DeselectIndex)
                OnSelect?.Invoke(Index, false);

            if (doActions)
                OnSelect?.Invoke(idx, !deselection);

            Index = deselection ? DeselectIndex : idx;
        }
        public void Deselect(bool doActions = true) {
            if (doActions) OnSelect?.Invoke(Index, false);
            Index = DeselectIndex;
        }


        private void FindItems() {
            Items = new List<ITab>(transform.GetComponentsInChildren<ITab>());
            foreach (var tab in Items) tab.SetGroup(this);
        }

        private void Refresh() {
            for (int i = 0; i < Items.Count; i++) Items[i].Select(i == Index);
        }

        private void Preselect() => Index = GetFirstActive();
        private int GetFirstActive() {
            for (int i = 0; i < Items.Count; i++)
                if (Items[i].Active)
                    return i;

            return DeselectIndex;
        }
        private int GetIndexOf(ITab needle) {
            for (int i = 0; i < Items.Count; i++)
                if (Items[i] == needle)
                    return i;

            return DeselectIndex;
        }
    }
}