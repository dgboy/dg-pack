namespace DG_Pack.Features.Customization.Code {
    public struct Stats {
        public float Strength { get; set; }
        public float Agility { get; set; }
        public float Intelligence { get; set; }
        public float Stamina { get; set; }

        public static Stats operator +(Stats a, Stats b) {
            return new Stats {
                Strength = a.Strength + b.Strength,
                Agility = a.Agility + b.Agility,
                Intelligence = a.Intelligence + b.Intelligence,
                Stamina = a.Stamina + b.Stamina,
            };
        }
        public static Stats operator *(Stats a, float m) {
            return new Stats {
                Strength = a.Strength * m,
                Agility = a.Agility * m,
                Intelligence = a.Intelligence * m,
                Stamina = a.Stamina * m,
            };
        }
    }

    public enum RaceType {
        Human, Elf, Orc, Goblin
    }

    public enum SpecType {
        Warrior, Thief, Mage, Scout
    }

}