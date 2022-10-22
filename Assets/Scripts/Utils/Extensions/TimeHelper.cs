using UnityEngine;

namespace EasyGames.Sources.Utils
{
    public static class TimeHelper
    {
        public static bool IsDelayPassed(float startTime, float delay, float checkTime)
        {
            return startTime + delay < checkTime;
        } 
        
        public static bool IsDelayPassed(float startTime, float delay)
        {
            return IsDelayPassed(startTime, delay, Time.time);
        } 
    }
}