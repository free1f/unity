using Freelf.Character.Interfaces;
using Freelf.Stats;
using Freelf.Character.DataTransfer;

namespace Freelf.Character
{
    public class CharacterStat : CharacterComponent, IAttached<StatData>, IStatUpdater
    {
        public VitalityStat HealthStat;
        public VitalityStat StaminaStat;

        private StatData _statData;
        
        public void Attached(ref StatData value)
        {
            _statData = value;
        }
        
        public override void Init()
        {
            HealthStat.CurrentValue.AddListener(CheckHealth);
            StaminaStat.CurrentValue.AddListener(CheckStamina);
            
            HealthStat.Change(HealthStat.MaxValue);
            StaminaStat.Change(StaminaStat.MaxValue);
        }
        
        private void CheckHealth(int value)
        {
            _statData.CurrentHealth = value;
        }

        private void CheckStamina(int value)
        {
            _statData.CurrentStamina = value;
        }

        public void SetHealth(int value)
        {
            var result = HealthStat.CurrentValue.Value + value;
            HealthStat.Change(result);
        }

        public void SetStamina(int value)
        {
            var result = StaminaStat.CurrentValue.Value + value;
            StaminaStat.Change(result);
        }
    }
}