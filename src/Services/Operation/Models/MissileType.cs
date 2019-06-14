using System.ComponentModel.DataAnnotations;

namespace Operation.Models
{
    public enum MissileType
    {
        [Display(Name = "Air-to-Air Missile")]
        AirToAir,
        [Display(Name = "Air-to-Surface Missile")]
        AirToSurface,
        [Display(Name = "Surface-to-Surface Missile")]
        SurfaceToSurface,
        [Display(Name = "Surface-to-Air Missile")]
        SurfaceToAir,
        [Display(Name = "Intercontinental Ballistic Missile")]
        ICBM,
        [Display(Name = "Submarine Launched Missile")]
        SubmarineLaunched
    }
}