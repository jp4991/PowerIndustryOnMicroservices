namespace Invoice.Domain.Services.Strategy
{
	public enum CreateIvoicesStrategyEnum
	{
		ParallelLoop,
		SingleThread,
		Throttling
	}
}
