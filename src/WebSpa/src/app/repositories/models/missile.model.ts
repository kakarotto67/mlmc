export class Missile {
  //public type: string;
  //public status: string;

  public typeString: string;
  public statusString: string;

  constructor(
    public missileId: number,
    public serviceIdentityNumber: string,
    public name: string,
    public type: number,
    public status: number,
    public inServiceDateStart: string,
    public inServiceDateEnd?: any
  ) {
    this.typeString = MissileTypeHelper.toDisplayValue(type);
    this.statusString = MissileStatusHelper.toDisplayValue(status);
  }
}

// TODO: Move stuff below into separate files
class MissileTypeHelper {
  public static toDisplayValue(intValue: number): string {
    switch (intValue) {
      case MissileType.AirToAir:
        return "Air-to-Air Missile";
      case MissileType.AirToSurface:
        return "Air-to-Surface Missile";
      case MissileType.SurfaceToSurface:
        return "Surface-to-Surface Missile";
      case MissileType.SurfaceToAir:
        return "Surface-to-Air Missile";
      case MissileType.ICBM:
        return "Intercontinental Ballistic Missile";
      case MissileType.SubmarineLaunched:
        return "Submarine Launched Missile";
      default:
        return "";
    }
  }
}

class MissileStatusHelper {
  public static toDisplayValue(intValue: number): string {
    switch (intValue) {
      case MissileStatus.InService:
        return "In Service";
      case MissileStatus.Launched:
        return "Launched";
      case MissileStatus.Decommisioned:
        return "Decommisioned";
      default:
        return "";
    }
  }
}

enum MissileType {
  AirToAir,
  AirToSurface,
  SurfaceToSurface,
  SurfaceToAir,
  ICBM,
  SubmarineLaunched
}

enum MissileStatus {
  InService,
  Launched,
  Decommisioned
}
