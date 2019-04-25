using System;

namespace Settlement.Domain.Services
{
	public interface ICreatePayerService
	{
		Guid CreatePayer(string name);
	}
}
