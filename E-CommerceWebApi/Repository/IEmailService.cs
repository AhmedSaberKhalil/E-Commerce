namespace E_CommerceWebApi.Repository
{
	public interface IEmailService
	{
		Task SendEmailAsync(string toEmail, string subject, string body);

	}
}
