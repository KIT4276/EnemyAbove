[System.Serializable]
public class GameData 
{
    public string playersName;
    public string lvl;
    public float maxHealth;
    public float coolDown;
    public float moveSpeed;
    public float stockAmmo;
    public float experience;
    public GameData(LVLLoader lVLLoader, Player player, ExperienceSystem experienceSystem, string name)
    {
        playersName = name;
        lvl = lVLLoader.GetPrevSceneName();
        maxHealth = player.GetMaxHealth();
        coolDown = player.GetCoolDown();
        moveSpeed = player.GetSpeed();
        stockAmmo = player.GetComponent<PlayersWeapon>().GetLeftAmmoStart();
        experience = experienceSystem.TotalExperience;
    }
}
