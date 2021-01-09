using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Utils
{
    public static int ParseInt(byte[]message, int startIndex)
    {

        byte[] intArray = new byte[4];
        Array.Copy(message, startIndex, intArray, 0, 4);


        return BitConverter.ToInt32(intArray, 0);
    }

    public static bool LineOfSight(int x1,int y1,int x2, int y2)
    {
        int t, x, y, absDeltaX, absDeltaY, signX, signY, deltaX, deltaY;

        deltaX = x2 - x1;
        deltaY = y2 - y1;

        absDeltaX = Mathf.Abs(deltaX);
        absDeltaY = Mathf.Abs(deltaY);

        signX = (deltaX < 0) ? -1 : 1;
        signY = (deltaY < 0) ? -1 : 1;

        Debug.LogWarning("Checking Los : "+x1+" "+y1+" to "+x2 +" "+y2);
        x = x1;
        y = y1;

        if(x1 == x2 && y1==y2)
        {
            return true;
        }
        if(absDeltaX > absDeltaY)
        {
            t = absDeltaY * 2 - absDeltaX;
            do
            {
                if (t>=0)
                {
                    y += signY;
                    t -= absDeltaX * 2;
                }
                x += signX;
                t += absDeltaY * 2;
                if (x == x2 && y == y2)
                {
                    return true;
                }
            } while (GameManager.gm.UnitOnTile(x, y) == null );
            return false;
        }
        else
        {
            t = absDeltaX * 2 - absDeltaY;
            do
            {
                if (t>=0)
                {
                    x += signX;
                    t -= absDeltaY * 2;
                }
                y += signY;
                t += absDeltaX * 2;
                if(x == x2 && y == y2)
                {
                    return true;
                }
            } while (GameManager.gm.UnitOnTile(x, y) == null);
            return false;
        }

    }

    public static bool LineOfSight2(int x1, int y1, int x2, int y2)
    {
        //True if target is origin

        int sourceX = x1, sourceY = y1, targetX = x2, targetY = y2;

        if (sourceX == targetX && sourceY == targetY)
        {
            return true;
        }

        int deltaX = targetX - sourceX;
        int deltaY = targetY - sourceY;


        
        if(Mathf.Abs(deltaY) <= Mathf.Abs(deltaX))
        {
            int yDirection = sourceY > targetY ? -1 : 1;
            float ySlope = (float)(deltaY) / (float)(deltaX);
            float yAtZero = sourceY - ySlope * sourceX;
            for (int currentX = sourceX; currentX < targetX; currentX++)
            {
                int currentY;
                float computeY = (yAtZero + currentX * ySlope);

                if (computeY - Mathf.Floor(computeY) == 0.5f)
                {
                    currentY = Mathf.CeilToInt(computeY);
                    if (currentX != targetX || currentY != targetY)
                    {
                        if (currentX != sourceX || currentY != sourceY)
                        {
                            if (GameManager.gm.UnitOnTile(currentX, currentY) != null)
                            {
                                return false;
                            }
                        }

                    }

                    currentY = Mathf.FloorToInt(computeY);
                    if (currentX != targetX || currentY != targetY)
                    {
                        if (currentX != sourceX || currentY != sourceY)
                        {
                            if (GameManager.gm.UnitOnTile(currentX, currentY) != null)
                            {
                                return false;
                            }
                        }

                    }
                }
                else
                {
                    currentY = Mathf.RoundToInt(computeY);
                    if (currentX != targetX || currentY != targetY)
                    {
                        if (currentX != sourceX || currentY != sourceY)
                        {
                            if (GameManager.gm.UnitOnTile(currentX, currentY) != null)
                            {
                                return false;
                            }
                        }

                    }
                }
            }
        }
        else
        {
            int xDirection = sourceX > targetX ? -1 : 1;
            float xSlope = (float)(deltaX) / (float)(deltaY);
            float xAtZero = sourceX - xSlope * sourceY;
            for (int currentY = sourceY; currentY < targetY; currentY++)
            {
                int currentX;
                float computeX = (xAtZero + xDirection * currentY * xSlope);

                if (computeX - Mathf.Floor(computeX) == 0.5f)
                {
                    currentX = Mathf.CeilToInt(computeX);
                    if (currentX != targetX || currentY != targetY)
                    {
                        if (currentX != sourceX || currentY != sourceY)
                        {
                            if (GameManager.gm.UnitOnTile(currentX, currentY) != null)
                            {
                                return false;
                            }
                        }

                    }

                    currentX = Mathf.FloorToInt(computeX);
                    if (currentX != targetX || currentY != targetY)
                    {
                        if (currentX != sourceX || currentY != sourceY)
                        {
                            if (GameManager.gm.UnitOnTile(currentX, currentY) != null)
                            {
                                return false;
                            }
                        }

                    }
                }
                else
                {
                    currentX = Mathf.RoundToInt(computeX);
                    if (currentX != targetX || currentY != targetY)
                    {
                        if (currentX != sourceX || currentY != sourceY)
                        {
                            if (GameManager.gm.UnitOnTile(currentX, currentY) != null)
                            {
                                return false;
                            }
                        }

                    }
                }
            }
        }
        

        return true;

    }

    public static bool LineOfSight3(int sourceX, int sourceY, int targetX, int targetY)
    {

        if (sourceX == targetX && sourceY == targetY)
        {
            return true;
        }

        if (sourceX == targetX)
        {
            return AlignedLos(sourceX, sourceY, targetX, targetY);
        }

        int xDirection = sourceX > targetX ? -1 : 1;
        if (sourceX > targetX)
        {
            int tmpX = sourceX, tmpY = sourceY;
            sourceX = targetX;
            sourceY = targetY;
            targetX = tmpX;
            targetY = tmpY;
        }

        int yDirection = sourceY > targetY ? -1 : 1;
        float ySlope = (float)(targetY - sourceY) / (float)(targetX - sourceX);
        float yAtZero = sourceY - ySlope * sourceX;

        int currentY = sourceY;
        for (int currentX = sourceX; currentX <= targetX; ++currentX)
        {
            
            double yMaxRaw =((currentX + 0.5) * ySlope + yAtZero);

            int yMax = (int)Math.Floor(yMaxRaw + 0.5f) ;

            for (; ; )
            {
                if (currentX == targetX && currentY == targetY)
                {
                    return true;
                }
                if (currentX != sourceX || currentY != sourceY)
                {
                    if (GameManager.gm.UnitOnTile(currentX, currentY) != null)
                    {
                        return false;
                    }
                }
                if(Math.Abs(yMaxRaw-currentY) == 0.5f)
                {
                    currentY += yDirection;
                    break;
                }

                if (currentY == yMax)
                {
                    break;

                }
                currentY += yDirection;
            }
        }

        for (; yDirection > 0 ? currentY < targetY : currentY > targetY; currentY += yDirection)
        {
            if (GameManager.gm.UnitOnTile(targetX, currentY) != null)
            {
                return false;
            }
        }

        return true;
    }

    public static bool AlignedLos(int x1, int y1, int x2, int y2)
    {
        int startY = Mathf.Min(y1, y2);
        int endY = Mathf.Max(y1, y2);

        for(int i = startY+1;i<endY;++i)
        {
            if(GameManager.gm.UnitOnTile(x1,i) != null)
            {
                return false;
            }
        }

        return true;
    }


}

