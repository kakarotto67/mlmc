using System.ComponentModel.DataAnnotations;

namespace Mlmc.Shared.Models
{
    public enum MissileStatus
    {
        [Display(Name = "In Service")]
        InService,
        // Not used for now
        [Display(Name = "Deployed")]
        Deployed,
        [Display(Name = "Launched")]
        Launched,
        [Display(Name = "Target Reached")]
        TargetReached,
        [Display(Name = "Target Missed")]
        TargetMissed,
        [Display(Name = "Decommisioned")]
        Decommisioned
    }
}