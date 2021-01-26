using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class CaptureAllPortalsEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public CaptureAllPortalsEffect() : base()
        {
        }

        public CaptureAllPortalsEffect(TargetSelector sources, TargetSelector targets, int value) 
            : base(sources, targets, value)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine(fightHandler.Entities[context.SourceId].Definition.Name+
                " captures all portal");

            Entity source = fightHandler.Entities[context.SourceId];
            foreach(Entity entity in fightHandler.Entities.Values)
            {
                if (entity.Team != source.Team && entity is Portal)
                {
                    entity.Team = source.Team;
                    fightHandler.FireEvent(new CapturePortalEvent(entity.Id, source.Id));
                    fightHandler.FireEvent(new EntityStatChangedEvent(((Unit)source).Owner.Id,
                        ((Unit)source).Owner.Id, Stat.gaugeArcane,
                        ((Unit)source).Owner.GetStat(Stat.gaugeArcane) + 1));
                }
            }
           


        }
        #endregion
    }
}
