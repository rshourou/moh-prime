using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Prime.Configuration.Auth;
using System.Threading;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.ManageEnrollee)]
    public class DiagnosticsController : PrimeControllerBase
    {
        private static readonly Process _process = Process.GetCurrentProcess();
        private static TimeSpan _oldCPUTime = TimeSpan.Zero;
        private static DateTime _lastMonitorTime = DateTime.UtcNow;
        private static DateTime _lastRpsTime = DateTime.UtcNow;
        private static double _cpu = 0, _rps = 0;
        private static readonly double RefreshRate = TimeSpan.FromSeconds(1).TotalMilliseconds;
        public static long Requests = 0;


        [HttpGet(Name = nameof(GetDiagnostics))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetDiagnostics()
        {
            var now = DateTime.UtcNow;
            _process.Refresh();

            var cpuElapsedTime = now.Subtract(_lastMonitorTime).TotalMilliseconds;

            if (cpuElapsedTime > RefreshRate)
            {
                var newCPUTime = _process.TotalProcessorTime;
                var elapsedCPU = (newCPUTime - _oldCPUTime).TotalMilliseconds;
                _cpu = elapsedCPU * 100 / Environment.ProcessorCount / cpuElapsedTime;

                _lastMonitorTime = now;
                _oldCPUTime = newCPUTime;
            }

            var rpsElapsedTime = now.Subtract(_lastRpsTime).TotalMilliseconds;
            if (rpsElapsedTime > RefreshRate)
            {
                _rps = Requests * 1000 / rpsElapsedTime;
                Interlocked.Exchange(ref Requests, 0);
                _lastRpsTime = now;
            }

            var diagnostics = new
            {
                PID = _process.Id,

                // The memory occupied by objects.
                Allocated = GC.GetTotalMemory(false),

                // The working set includes both shared and private data. The shared data includes the pages that contain all the
                // instructions that the process executes, including instructions in the process modules and the system libraries.
                WorkingSet = _process.WorkingSet64,

                // The value returned by this property represents the current size of memory used by the process, in bytes, that
                // cannot be shared with other processes.
                PrivateBytes = _process.PrivateMemorySize64,

                // The number of generation 0 collections
                Gen0 = GC.CollectionCount(0),

                // The number of generation 1 collections
                Gen1 = GC.CollectionCount(1),

                // The number of generation 2 collections
                Gen2 = GC.CollectionCount(2),

                CPU = _cpu,

                RPS = _rps
            };

            return new ObjectResult(diagnostics);
        }

        [HttpPost("gc-collect", Name = nameof(GCCollect))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult GCCollect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return NoContent();
        }
    }
}
