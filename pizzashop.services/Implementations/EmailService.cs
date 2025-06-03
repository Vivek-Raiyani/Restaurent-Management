using MailKit.Net.Smtp;
using MimeKit;
using pizzashop.data.ViewModels;
using pizzashop.services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using pizzashop.data.Models;

namespace pizzashop.services.Implementations;

public class EmailService : IEmailService
{
    public void SendMail(string email, string reseturl){

        Console.WriteLine(email);
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Your Name", "test.dotnet@etatvasoft.com"));
        message.To.Add(new MailboxAddress("Recipient Name", email));
        message.Subject = "Test Email";

        Console.WriteLine(reseturl);
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $@"
            <div >
                <header style='background-color: blue; padding: 10px; text-align: center;display: flex; justify-content: center; align-items: center;'>
                    <img src='http://localhost:5074/images/pizzashop_logo.png' alt='logo' style='width: 100px; height: 100px;'>
                    <h1>Pizzashop</h1>  
                </header>
                <main style='padding: 10px;'>
                    <p>Pizza Shop</p>
                    <p>Please Click the <a href='{reseturl}' style='color: blue;'><u>link</u></a> below to reset your password</p>
                    <br>
                    <p>If you encounter any issues, or have any question, please do not hesitate to contact our support team.</p>
                    <br>
                    <p><span style='color: orange'>Important Note:</span> For security reasons, this link will expire in 24 hours. if you did not
                    request a password reset, please ignore this email or support our contact team immediatly</p>
                </main>

            ";

        

        message.Body = bodyBuilder.ToMessageBody();


        using (var client = new SmtpClient())
        {
            client.Connect("mail.etatvasoft.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate("test.dotnet@etatvasoft.com", "P}N^{z-]7Ilp");

            client.Send(message);
            client.Disconnect(true);
        }

    }
    public void NewUserMail(ProfileVM user){

        // Console.WriteLine(email);
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Your Name", "test.dotnet@etatvasoft.com"));
        message.To.Add(new MailboxAddress("Recipient Name", user.Email));
        message.Subject = "Test Email";

        // Console.WriteLine(reseturl);
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $@"
                    <div >
                     <header style='background-color:  #0066A8 ;color:white; padding: 10px; text-align: center;display: flex; justify-content: center; align-items: center;'>
                        <img src='http://localhost:5074/images/pizzashop_logo.png' alt='logo' style='width: 100px; height: 100px;'>
                        <h1>Pizzashop</h1>  
                     </header>
                    <main style='padding: 10px;'>
                        <p>Welcome to Pizzashop Pizza Shop</p>
                        <p>Please find the details below for login into your account</p>
                        <br>
                        <div style='border:solid black;'>
                            <div style='margin-left:10px; font-weight: 500;'>
                                <p>Login Details:<p>
                                <p>Username:{user.Email}</p>
                                <p>Temporary Password:{user.Password}</p>
                            </div>
                        </div>

                        <p>If you encounter any issues or have any questions, please do not hastitate to contact our support team.</p>
                    </main>
                    ";

        

        message.Body = bodyBuilder.ToMessageBody();


        using (var client = new SmtpClient())
        {
            client.Connect("mail.etatvasoft.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate("test.dotnet@etatvasoft.com", "P}N^{z-]7Ilp");

            client.Send(message);
            client.Disconnect(true);
        }

    }

    
}




//   bodyBuilder.HtmlBody = $@"
        //             <div >
        //              <header style='background-color:  #0066A8 ;color:white; padding: 10px; text-align: center;display: flex; justify-content: center; align-items: center;'>
        //                 <img src='http://localhost:5225/images/logos/pizzashop_logo.png' alt='logo' style='width: 100px; height: 100px;'>
        //                 <h1>Pizzashop</h1>  
        //              </header>
        //             <main style='padding: 10px;'>
        //                 <p>Welcome to Pizzashop Pizza Shop</p>
        //                 <p>Please find the details below for login into your account</p>
        //                 <br>
        //                 <div style='border:solid black;'>
        //                     <div style='margin-left:10px; font-weight: 500;'>
        //                         <p>Login Details:<p>
        //                         <p>Username:{user.Email}</p>
        //                         <p>Temporary Password:{user.PasswordHash}</p>
        //                     </div>
        //                 </div>

        //                 <p>If you encounter any issues or have any questions, please do not hastitate to contact our support team.</p>
        //             </main>
        //             ";