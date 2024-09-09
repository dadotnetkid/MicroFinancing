using System.IO.Ports;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MicroFinancing.Services;

public class SmsService : ISmsService
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IRepository<Customers, long> _customerRepository;

    public SmsService(IConfiguration configuration,
        IWebHostEnvironment hostEnvironment,
        IRepository<Customers, long> customerRepository)
    {
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
        _customerRepository = customerRepository;
    }

    public void SendNewlyCreateCustomer(string phoneNumber, string customerName)
    {
        var path = Path.Combine(_hostEnvironment.WebRootPath, "NewCustomerCreated.txt");

        var file = File.ReadAllText(path);
        file = file.Replace("[CustomerName]", customerName);
        SendSms(phoneNumber, file);
    }

    public void SendPaymentConfirmation(long customerId, string? amount)
    {
        var path = Path.Combine(_hostEnvironment.WebRootPath, "SendPaymentConfirmation.txt");

        var customer = _customerRepository.Entity
            .AsNoTracking()
            .FirstOrDefault(c => c.Id == customerId);

        var lines = File.ReadAllText(path);


        SendSms(customer.PhoneNumber, lines);
    }

    public void SendSms(string phoneNumber, string messages)
    {
        using var serialPort = ConfigurePort();
        var res = string.Empty;
        serialPort.WriteLine($"AT\r\n");
        Thread.Sleep(500);
        res = serialPort.ReadExisting();
        Console.WriteLine(res);
        serialPort.Write($"AT+CMGF=1\r\n");
        res = serialPort.ReadExisting();
        Console.WriteLine(res);
        Thread.Sleep(500);
        serialPort.Write($"AT+CMGS=\"{phoneNumber}\"\r\n");
        res = serialPort.ReadExisting();
        Console.WriteLine(res);
        Thread.Sleep(500);
        serialPort.WriteLine($"{messages}");
        res = serialPort.ReadExisting();
        Console.WriteLine(res);
        serialPort.Write([26], 0, 1);

        string response = serialPort.ReadExisting();
        Console.WriteLine($"Modem response: {response}");
        serialPort.Close();
    }


    private SerialPort ConfigurePort()
    {
        var serialPort = new SerialPort
        {
            PortName = _configuration.GetValue<string>("Sms:Port"),
            BaudRate = 9600,
            DataBits = 8,
            DtrEnable = true,
            RtsEnable = true,
            NewLine = Environment.NewLine,
            Handshake = Handshake.RequestToSend,
            StopBits = StopBits.One,
            Parity = Parity.Even
        };

        if (!serialPort.IsOpen)
        {
            serialPort.Open();
        }

        return serialPort;
    }
}