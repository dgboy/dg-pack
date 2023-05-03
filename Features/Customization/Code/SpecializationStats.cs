using System;

namespace DG_Pack.Features.Customization.Code {
    public class SpecializationStats : IStatsProvider {
        private readonly SpecType _spec;
        private readonly IStatsProvider _provider;

        // public SpecializationStats(RaceType race) {
        //     _race = race;
        // }
        public SpecializationStats(IStatsProvider provider, SpecType spec) {
            _provider = provider;
            _spec = spec;
        }

        public Stats GetStats() {
            return _spec switch {
                SpecType.Warrior => new Stats {
                    Strength = 15,
                    Agility = 15,
                    Intelligence = 15,
                    Stamina = 15,
                },
                SpecType.Thief => new Stats {
                    Strength = 10,
                    Agility = 10,
                    Intelligence = 10,
                    Stamina = 10,
                },
                SpecType.Mage => new Stats {
                    Strength = 20,
                    Agility = 20,
                    Intelligence = 20,
                    Stamina = 20,
                },
                SpecType.Scout => new Stats {
                    Strength = 5,
                    Agility = 5,
                    Intelligence = 5,
                    Stamina = 5,
                },
                _ => throw new ArgumentOutOfRangeException($"Race {_spec} doesn't implemented!")
            };
        }
    }
}