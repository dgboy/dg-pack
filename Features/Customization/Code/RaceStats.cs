using System;

namespace DG_Pack.Features.Customization.Code {
    public class RaceStats : IStatsProvider {
        private RaceType _race;

        public RaceStats(RaceType race) {
            _race = race;
        }

        public Stats GetStats() {
            return _race switch {
                RaceType.Human => new Stats {
                    Strength = 15,
                    Agility = 15,
                    Intelligence = 15,
                    Stamina = 15,
                },
                RaceType.Elf => new Stats {
                    Strength = 10,
                    Agility = 10,
                    Intelligence = 10,
                    Stamina = 10,
                },
                RaceType.Orc => new Stats {
                    Strength = 20,
                    Agility = 20,
                    Intelligence = 20,
                    Stamina = 20,
                },
                RaceType.Goblin => new Stats {
                    Strength = 5,
                    Agility = 5,
                    Intelligence = 5,
                    Stamina = 5,
                },
                _ => throw new ArgumentOutOfRangeException($"Race {_race} doesn't implemented!")
            };
        }
    }

}