namespace Player
{
    public struct StatsContainer
    {
        public int CurrentHealth;
        public int CurrentMana;
        public int MaxHealth;
        public int MaxMana;
        public int Experience;
        public int Money;

        public StatsContainer(int maxHealth, int currentHealth, int maxMana, 
            int currentMana, int experience, int money) 
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
            MaxMana = maxMana;
            CurrentMana = currentMana;
            Experience = experience;
            Money = money;
        }
    }
}
