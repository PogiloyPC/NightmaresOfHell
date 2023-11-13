using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeController : MonoBehaviour
{
    [SerializeField] private UpgraderTurret[] _upgradersTurret;

    [SerializeField] private List<ConfigTurretStats> _configsTurretStats;

    private StatsConfigData _statsConfigData;

    private void Start()
    {
        _statsConfigData = new StatsConfigData();

        for (int i = 0; i < _configsTurretStats.Count; i++)
            _statsConfigData.AddConfigStats(_configsTurretStats[i]);        
    }

    public IUpgraderTurret[] GetAllUpgradersTurret() => _upgradersTurret;

    public void InitUpgradeView(IPanelTurretUpgrade panel)
    {
        foreach (UpgraderTurret upgraderTurret in _upgradersTurret)
            upgraderTurret.InitUpgrader(panel);
    }
}

