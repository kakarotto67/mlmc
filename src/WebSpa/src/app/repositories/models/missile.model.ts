export class Missile {
  public MissileId: number;
  public ServiceIdentityNumber: string;
  public Name: string;
  public Type: MissileType;
  public InServiceDateStart: any;
  public InServiceDateEnd: any;
  public Status: MissileStatus;
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
