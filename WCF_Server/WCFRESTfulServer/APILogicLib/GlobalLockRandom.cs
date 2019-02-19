using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib
{
    public static class GlobalLockRandom
    {
        private static Random GlobalRandNumber = new Random();

        /// <summary>
        /// int 범위 내의 무작위 랜덤 구하기
        /// </summary>
        /// <returns></returns>
        public static int GetRandom()
        {
            lock (GlobalRandNumber) return GlobalRandNumber.Next();
        }

        /// <summary>
        /// 0부터 max을 포함하지 않는 범위에서의 랜덤 구하기
        /// 0 포함, max는 불포함
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandom(int max)
        {
            lock (GlobalRandNumber) return GlobalRandNumber.Next(max);
        }

        /// <summary>
        /// min부터 max를 포함하지 않는 범위에서 랜덤 구하기
        /// min 포함, max는 불포함
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandom(int min, int max)
        {
            lock (GlobalRandNumber) return GlobalRandNumber.Next(min, max);
        }

        /// <summary>
        /// 0부터 1 사이의 double형 랜덤 구하기
        /// </summary>
        /// <returns></returns>
        public static double GetRandomDouble()
        {
            lock (GlobalRandNumber) return GlobalRandNumber.NextDouble();
        }
    }
}
