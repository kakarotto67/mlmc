using Mlmc.Shared.Models;
using System;

namespace Mlmc.Shared.Events
{
    public sealed class LaunchedMissileCurrentStatusEvent : BaseEvent
    {
        public Guid MissileServiceIdentityNumber { get; set; }
        public string MissileName { get; set; }
        public MissileStatus MissileStatus { get; set; }
        public Location MissileGpsLocation { get; set; }
        public DateTime InformationPostedDate { get; set; }
        public bool IsFinished { get; private set; }

        public void SetFinalInfo(Location targetLocation)
        {
            if (MissileStatus != MissileStatus.Launched || IsFinished)
            {
                throw new InvalidOperationException();
            }

            MissileGpsLocation = targetLocation ?? throw new ArgumentNullException();
            // TODO: Add random simulation (reached or missied)
            MissileStatus = MissileStatus.TargetReached;
            InformationPostedDate = DateTime.UtcNow;

            IsFinished = true;
        }

        public void SetIntermediaryInfo(Location currentLocation)
        {
            if(MissileStatus != MissileStatus.Launched || IsFinished)
            {
                throw new InvalidOperationException();
            }

            MissileGpsLocation = currentLocation ?? throw new ArgumentNullException();
            InformationPostedDate = DateTime.UtcNow;
        }
    }
}