using UnityEngine.UI;
public class ViwerExperienceLevel : Image
{
    public void DisplayExperiencePlayer(IPlayerExperience playerExperience)
    {
        fillAmount = playerExperience.GetCurrentExperience() / playerExperience.GetFinishExperience();
    }
}

