using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace TestEWS.Tests
{
    class MessageCardTest: ITest
    {
        void ITest.Run()
        {
            // desing message here: https://messagecardplayground.azurewebsites.net/
            string mailBody = @"
<html>
    <head>
		<meta http-equiv=""Content-Type"" content=""text/html; charset=utf8"">
		<script type=""application/ld+json"">{
		""@context"": ""http://schema.org/extensions"",
		""@type"": ""MessageCard"",
		""hideOriginalBody"": ""false"",
        ""themeColor"": ""0075FF"",
		""sections"": [
            {
             ""heroImage"": {
                ""image"": ""http://www.lanteria.com/sites/all/themes/lanteria/images/head/logo.svg""
             }
            },
			{
            ""title"": ""Vacation request is pending your approval"",
            ""text"": ""Please review the vacation request below."",
			""facts"": [
				{
				""name"": ""Employee"",
				""value"": ""Sergey Balog""
				},
				{
				""name"": ""From"",
				""value"": ""1 September 2017 AM""
				},
				{
				""name"": ""Return to work"",
				""value"": ""10 September 2017 AM""
				}
			]
			},
            {
            ""potentialAction"": [
			{
			""@type"": ""OpenUri"",
			""name"": ""Approve"",
			""targets"": [ { ""os"": ""default"", ""uri"": ""http://google.com/"" } ]
			},
			{
			""@type"": ""OpenUri"",
			""name"": ""View Details"",
			""targets"": [ { ""os"": ""default"", ""uri"": ""http://hr.lanteria.com/es/SitePages/Absence/MyTeamAbsences.aspx"" } ]
			}
		]
        },
            {
            ""startGroup"": ""true"",
            ""activitySubtitle"": ""This message was created by an automated workflow in Lanteria HR. Do not reply.""
            }
		]
		}
		</script>
	</head>
	<body>
		Please approve vacation request.
	</body>
</html>";
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("sbalog@lanteria.net", "@WSX3edc");
            client.Port = 587;
            client.EnableSsl = true;
            client.Host = "smtp.office365.com";
            mail.To.Add("sbalog@lanteria.net"); // <-- this one
            mail.From = new MailAddress("sbalog@lanteria.net");
            mail.Subject = "this is a test email.";
            mail.IsBodyHtml = true;
            mail.Body = mailBody;
            client.Send(mail);
            Console.WriteLine("Email sent successfully!");
        }


        string ITest.Title
        {
            get { return "Outlook Actionable Message Test"; }
        }
    }
}
