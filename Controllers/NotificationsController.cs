using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

using   ms_notifications_UrbanNav.Models;
namespace ms_notifications_UrbanNav.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationsController : ControllerBase
{
[Route("mail")]
   [HttpPost]

public async Task<ActionResult>SendMail(MailModel data){
    
     var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY_URBAN_NAV");
  
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("cristian.1701421857@ucaldas.edu.co","Cristian Camilo Osorio");
            var subject =data.emailSubject;
            var to =  new EmailAddress(data.destinationEmail,data.destinationName);
            var plainTextContent = "plain text content";
            var htmlContent =  data.mailContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            if(response.StatusCode==System.Net.HttpStatusCode.Accepted){
                return Ok("email sent to the address" +data.destinationEmail);
            }else{

                return BadRequest("error sending email to the address" +data.destinationEmail);
            }



}

}
