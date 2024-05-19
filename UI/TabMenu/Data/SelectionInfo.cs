using System;

namespace DG_Pack.UI.TabMenu.Data {
    [Serializable]
    public class SelectionInfo<T> {
        public T on;
        public T off;

        public SelectionInfo(T value) {
            on = off = value;
        }
        public SelectionInfo(T on, T off) {
            this.on = on;
            this.off = off;
        }
        public SelectionInfo(SelectionInfo<T> info) {
            on = info.on;
            off = info.off;
        }


        public T GetBy(bool selection) => selection ? on : off;
    }
}