public enum RoomEventTriggerCondition
{
	ON_ENTER = 0,
	ON_EXIT = 1,
	ON_ENEMIES_CLEARED = 2,
	ON_ENTER_WITH_ENEMIES = 3,
	ON_ONE_QUARTER_ENEMY_HP_DEPLETED = 8,
	ON_HALF_ENEMY_HP_DEPLETED = 12,
	ON_THREE_QUARTERS_ENEMY_HP_DEPLETED = 16,
	ON_NINETY_PERCENT_ENEMY_HP_DEPLETED = 20,
	TIMER = 30,
	SHRINE_WAVE_A = 40,
	SHRINE_WAVE_B = 41,
	SHRINE_WAVE_C = 42,
	NPC_TRIGGER_A = 60,
	NPC_TRIGGER_B = 61,
	NPC_TRIGGER_C = 62,
	ENEMY_BEHAVIOR = 80,
	SEQUENTIAL_WAVE_TRIGGER = 100
}