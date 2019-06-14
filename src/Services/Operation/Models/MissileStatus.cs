using System.ComponentModel.DataAnnotations;

namespace Operation.Models
{
    public enum MissileStatus
    {
        [Display(Name = "In Service")]
        InService,
        [Display(Name = "Launched")]
        Launched,
        [Display(Name = "Decommisioned")]
        Decommisioned
    }
}