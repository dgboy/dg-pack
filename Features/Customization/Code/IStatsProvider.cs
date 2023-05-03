namespace DG_Pack.Features.Customization.Code {
    public interface IStatsProvider {
        Stats GetStats();
    }

    abstract class StatsDecorator : IStatsProvider {
        protected readonly IStatsProvider _wrappedEntity;
        
        protected StatsDecorator(IStatsProvider wrappedEntity) {
            _wrappedEntity = wrappedEntity;
        }

        public Stats GetStats() {
            return GetStatsInternal();
        }

        protected abstract Stats GetStatsInternal();
    }
}