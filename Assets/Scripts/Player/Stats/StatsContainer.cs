namespace Player
{
    public struct StatsContainer
    {
        public int CurrentHealth;
        public int CurrentMana;
        public int MaxHealth;
        public int MaxMana;

        public StatsContainer(int maxHealth = 0, int currentHealth = 0, int maxMana = 0, int currentMana = 0)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
            MaxMana = maxMana;
            CurrentMana = currentMana;
        }
    }
}
