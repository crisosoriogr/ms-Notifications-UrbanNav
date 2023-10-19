using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

using   ms_notifications_UrbanNav.Models;
namespace ms_notifications_UrbanNav.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationsController : ControllerBase
{
[Route("mail-welcome")]
   [HttpPost]

public async Task<ActionResult>SendWelcomeEmail(MailModel data){
    
     var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY_URBAN_NAV");
  
            var client = new SendGridClient(apiKey);
    SendGridMessage msg=this.CreatebaseMessage(data);
            msg.SetTemplateId(Environment.GetEnvironmentVariable("WELCOME_SENGRID_TEMPLATE_ID"));
            msg.SetTemplateData(
                new {
                    name=data.destinationName,
                    message="Welcome to Urban Nav, we are glad to have you here"


                }
            );
            var response = await client.SendEmailAsync(msg);
            if(response.StatusCode==System.Net.HttpStatusCode.Accepted){
                return Ok("email sent to the address" +data.destinationEmail);
            }else{

                return BadRequest("error sending email to the address" +data.destinationEmail);
            }





}


private SendGridMessage CreatebaseMessage(MailModel data){

            var from = new EmailAddress(Environment.GetEnvironmentVariable("EMAIL_FROM"),Environment.GetEnvironmentVariable("NAME_FROM"));
            var subject =data.emailSubject;
            var to =  new EmailAddress(data.destinationEmail,data.destinationName);
            var plainTextContent = "plain text content";
            var htmlContent =  data.mailContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return msg;
           


}

}
