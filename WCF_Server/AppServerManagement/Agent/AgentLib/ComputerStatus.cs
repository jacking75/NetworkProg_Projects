using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices.ComTypes;
using System.Diagnostics;


namespace AgentLib
{
    class ComputerStatus
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetSystemTimes(out FILETIME lpIdleTime,
                    out FILETIME lpKernelTime, out FILETIME lpUserTime);

        static FILETIME _prevSysKernel;
        static FILETIME _prevSysUser;
        static FILETIME _prevSysIdle;

        static TimeSpan _prevProcTotal;


        public static int 메모리_사용량 = 0;
        public static float 전체_CPU_사용량 = 0.0f;
        public static float 프로세스_CPU_사용량 = 0.0f;


        public static void Init()
        {
            _prevProcTotal = TimeSpan.MinValue;
        }

        public static void GetStatus(Process process, string exename)
        {
            프로세스_CPU_사용량 = 0.0f;
            전체_CPU_사용량 = 0.0f;
            메모리_사용량 = 0;

            if (process != null)
            {
                프로세스_CPU_사용량 = GetUsage(process, out 전체_CPU_사용량);

                var objMemory = new System.Diagnostics.PerformanceCounter("Process", "Working Set - Private", exename);
                메모리_사용량 = (int)objMemory.NextValue() / 1000000;
            }
        }

        static float GetUsage(Process process, out float processorCpuUsage)
        {
            processorCpuUsage = 0.0f;

            float _processCpuUsage = 0.0f;
            FILETIME sysIdle, sysKernel, sysUser;

            TimeSpan procTime = process.TotalProcessorTime;

            if (!GetSystemTimes(out sysIdle, out sysKernel, out sysUser))
            {
                return 0.0f;
            }

            if (_prevProcTotal != TimeSpan.MinValue)
            {
                ulong sysKernelDiff = SubtractTimes(sysKernel, _prevSysKernel);
                ulong sysUserDiff = SubtractTimes(sysUser, _prevSysUser);
                ulong sysIdleDiff = SubtractTimes(sysIdle, _prevSysIdle);

                ulong sysTotal = sysKernelDiff + sysUserDiff;
                long kernelTotal = (long)(sysKernelDiff - sysIdleDiff);

                if (kernelTotal < 0)
                {
                    kernelTotal = 0;
                }

                processorCpuUsage = (float)((((ulong)kernelTotal + sysUserDiff) * 100.0) / sysTotal);

                long procTotal = (procTime.Ticks - _prevProcTotal.Ticks);

                if (sysTotal > 0)
                {
                    _processCpuUsage = (short)((100.0 * procTotal) / sysTotal);
                }
            }

            _prevProcTotal = procTime;
            _prevSysKernel = sysKernel;
            _prevSysUser = sysUser;
            _prevSysIdle = sysIdle;

            return _processCpuUsage;
        }

        static UInt64 SubtractTimes(FILETIME a, FILETIME b)
        {
            ulong aInt = ((ulong)a.dwHighDateTime << 32) | (uint)a.dwLowDateTime;
            ulong bInt = ((ulong)b.dwHighDateTime << 32) | (uint)b.dwLowDateTime;

            return aInt - bInt;
        }

        
        
    }

    
}
