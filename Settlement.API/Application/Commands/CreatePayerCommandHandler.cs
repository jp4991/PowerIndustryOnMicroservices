using MediatR;
using Settlement.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Settlement.API.Application.Commands
{
	public class CreatePayerCommandHandler : IRequestHandler<CreatePayerCommand, CreatePayerResponse>
	{
		private readonly ICreatePayerService _createPayerService;

		public CreatePayerCommandHandler(ICreatePayerService createPayerService)
		{
			_createPayerService = createPayerService;
		}

		public Task<CreatePayerResponse> Handle(CreatePayerCommand request, CancellationToken cancellationToken)
		{
			var response = new CreatePayerResponse();
			try
			{
				response.PayerId = _createPayerService.CreatePayer(request.Name, request.PriceDiscount);
				response.Success = true;

				return Task.FromResult(response);
			}
			catch (Exception)
			{
				response.Success = false;
				response.ErrorDescription = "Wystąpił błąd w trakcie tworzenia płatnika.";
				return Task.FromResult(response);
			}
		}
	}
}
