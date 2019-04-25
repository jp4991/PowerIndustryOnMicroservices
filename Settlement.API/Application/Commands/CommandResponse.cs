namespace Settlement.API.Application.Commands
{
	public abstract class CommandResponse
	{
		public bool Success { get; set; }
		public string ErrorDescription { get; set; }
	}
}
