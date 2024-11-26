using System.Net.Http.Json;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace MicroFinancing.Services;

public class SmsApiService : ISmsService
{
    private readonly HttpClient _httpClient;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IRepository<Customers, long> _customerRepository;

    public SmsApiService(IWebHostEnvironment hostEnvironment,
        IRepository<Customers, long> customerRepository,
        IHttpClientFactory httpClientFactory)
    {
        _hostEnvironment = hostEnvironment;
        _customerRepository = customerRepository;

        _httpClient = httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
    }

   

    public async Task SendSms(string phoneNumber, string messages)
    {
        try
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return;
            }

            await _httpClient.PostAsJsonAsync("/api/Sms/Send",
                                              new
                                              {
                                                  number = phoneNumber,
                                                  message = messages
                                              });
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task SendNewlyCreateCustomer(string phoneNumber, string customerName)
    {
        var path = Path.Combine(_hostEnvironment.WebRootPath, "NewCustomerCreated.txt");

        var file = File.ReadAllText(path);
        file = file.Replace("[CustomerName]", customerName);
        await SendSms(phoneNumber, file);
    }

    public async Task SendPaymentConfirmation(long customerId,
                                              Payment payment)
    {

        var path = Path.Combine(_hostEnvironment.WebRootPath, "SendPaymentConfirmation.txt");

        var customer = await _customerRepository.Entity
            .AsNoTracking()
            .Where(c => c.Id == customerId)
            .Select(x => new
            {
                FullName = x.FullName,
                x.PhoneNumber,
                Balance = x.Lending.Where(c => c.Id == payment.LendingId).Sum(l => l.TotalCredit) - 
                          x.Payments.Where(c => c.LendingId == payment.LendingId).Sum(p => p.PaymentAmount)
            }).FirstOrDefaultAsync();



        var lines = await File.ReadAllTextAsync(path);

        lines = lines.Replace("[Amount]", payment.PaymentAmount?.ToString("n2"))
            .Replace("[CustomerName]", customer.FullName)
            .Replace("[Balance]", customer.Balance?.ToString("n2"));

        await SendSms(customer.PhoneNumber, lines);
    }

    public async Task SendRestructureToAdmin(string? customers)
    {
        var path = Path.Combine(_hostEnvironment.WebRootPath, "RestructureTemplateAdmin.txt");

        var lines = await File.ReadAllTextAsync(path);

        lines = lines.Replace("[customers]", customers);

        await SendSms("09179602390", lines);
    }

    public async Task SendRestructureToCustomer(string phoneNumber, string customerName)
    {
        var path = Path.Combine(_hostEnvironment.WebRootPath, "RestructureTemplate.txt");

        var lines = await File.ReadAllTextAsync(path);

        lines = lines.Replace("[CustomerName]", customerName);

        await SendSms("09179602390", lines);
    }
}