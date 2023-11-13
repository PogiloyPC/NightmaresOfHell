using System.Collections.Generic;

public class StatsConfigData
{
    private static List<ConfigTurretStats> _dataConfigStats = new List<ConfigTurretStats>();

    public void AddConfigStats(ConfigTurretStats configStats) => _dataConfigStats.Add(configStats);

    public static ConfigTurretStats GetConfigStats(NameTurret nameTurret)
    {
        for (int i = 0; i < _dataConfigStats.Count; i++)
            if (nameTurret == _dataConfigStats[i].GetNameTurretStatsConfig())
                return _dataConfigStats[i];

        return null;
    }
}

