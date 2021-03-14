using System;
using System.Collections.Generic;
using System.Text;

namespace Myths_Server
{
    class PullEffect : Effect
    {

        #region Attributes

        #endregion

        #region Getters & Setters

        #endregion

        #region Constructor
        public PullEffect() : base()
        {
        }

        public PullEffect(TargetSelector sources, TargetSelector targets, List<int> values) 
            : base(sources, targets, values)
        {

        }
        #endregion

        #region Methods
        public override void ExecuteOnTarget(int targetId, Context context, FightHandler fightHandler)
        {

            Console.WriteLine( fightHandler.Entities[targetId].Definition.Name+" is pushed");

            Entity target = fightHandler.Entities[targetId];
            int sourceX = fightHandler.Entities[context.SourceId].GetStat(Stat.x);
            int sourceY = fightHandler.Entities[context.SourceId].GetStat(Stat.y);
            int targetX = fightHandler.Entities[targetId].GetStat(Stat.x);
            int targetY = fightHandler.Entities[targetId].GetStat(Stat.y);
            int realPush = 0;
            if(sourceX == targetX)
            {
                //North
                if(sourceY < targetY)
                {
                    for(int i=1;i<= values[0]; i++)
                    {
                        if (targetY - i >= 0 && fightHandler.UnitOnTile(sourceX, targetY - i) == null)
                        {
                            realPush = i;

                        }
                        else
                        { break; }
                    }
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                        Stat.y, targetY - realPush));

                    fightHandler.FireEvent(new EntityMovedEvent(targetId, targetId, sourceX, targetY - realPush));
                }
                else //South
                {
                    for (int i = 1; i <= values[0]; i++)
                    {
                        if (targetY + i <= 4 && fightHandler.UnitOnTile(sourceX, targetY + i) == null)
                        {
                            realPush = i;

                        }
                        else
                        { break; }
                    }
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                        Stat.y, targetY + realPush));

                    fightHandler.FireEvent(new EntityMovedEvent(targetId, targetId, sourceX, targetY + realPush));
                }
            }
            else if (sourceY == targetY)
            {
                //West
                if (sourceX < targetX)
                {
                    for (int i = 1; i <= values[0]; i++)
                    {
                        if (targetX - i >= 0 && fightHandler.UnitOnTile(targetX - i, sourceY) == null)
                        {
                            realPush = i;

                        }
                        else
                        { break; }
                    }
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                        Stat.x, targetX - realPush));

                    fightHandler.FireEvent(new EntityMovedEvent(targetId, targetId, targetX - realPush, sourceY));
                }
                else //East
                {
                    for (int i = 1; i <= values[0]; i++)
                    {
                        if (targetX + i <=6 && fightHandler.UnitOnTile(targetX + i, sourceY) == null)
                        {
                            realPush = i;

                        }
                        else
                        { break; }
                    }
                    fightHandler.FireEvent(new EntityStatChangedEvent(targetId, targetId,
                        Stat.x, targetX + realPush));

                    fightHandler.FireEvent(new EntityMovedEvent(targetId, targetId, targetX + realPush, sourceY));
                }
            }

        }
        #endregion
    }
}
