namespace Core.Commands.Commands
{
    public interface ICommand<in TContext, TResult>
    {
        /// <summary>
        /// Executes some BL item.
        /// </summary>
        /// <param name="context">The context, can contain required parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TResult> ExecuteAsync(TContext context, CancellationToken cancellationToken = default);
    }
}
