namespace DG_Pack.Services.Log {
    public static class LogHelper {
        public static string GetValue(this Dye scene) => scene switch {
            Dye.Red => "red",
            Dye.Green => "green",
            Dye.Blue => "#00aaff",
            Dye.Yellow => "yellow",
            Dye.Magenta => "#ff00ff",
            Dye.Pink => "#ffc0cb",
            Dye.Orange => "#ffa500",
            Dye.Black => "black",
            Dye.White => "white",
            _ => "grey",
        };
        public static string ToColor(this LogType scene) => scene switch {
            LogType.Init => "cyan",
            LogType.Clear => "white",
            LogType.Info => "yellow",
            LogType.Transition => "#00aaffff",
            LogType.User => "yellow",
            LogType.Save => "green",
            LogType.Cheat => "black",
            _ => "gray",
        };


        public static string ClassName(this object sender) => sender.GetType().Name;
        public static string ToColor(this string msg, Dye dye = Dye.None) => $"<color={dye.GetValue()}>{msg}</color>";
    }
}