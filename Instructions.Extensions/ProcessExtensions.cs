using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Instructions.Extensions
{
    public static class ProcessExtensions
    {
        /// <summary>
        /// Wait for a process to end asynchronously
        /// </summary>
        /// <param name="process">The process to wait</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/> to await</returns>
        public static Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default)
        {
            if (process.HasExited)
                return Task.CompletedTask;

            TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();

            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => taskCompletionSource.TrySetResult(null);

            if (cancellationToken != default)
                cancellationToken.Register(() => taskCompletionSource.SetCanceled());

            return process.HasExited ? Task.CompletedTask : taskCompletionSource.Task;
        }
    }
}
