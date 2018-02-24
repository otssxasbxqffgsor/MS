﻿/**
* Sample code for the GetOpenReturnTemplateDetails Canada Post service.
* 
* The GetOpenReturnTemplateDetails service is used to retrieve the information
* you originally provided when you called Create Open Return Template to create
* the template (e.g. the number of labels you specified for this template; the
* receiver). This call also provides the number of remaining labels for this
* template.
*
* This sample is configured to access the Developer Program sandbox environment. 
* Use your development key username and password for the web service credentials.
* 
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.Xml.Serialization;
using System.IO;

namespace GetOpenReturnTemplateDetails
{
    class GetOpenReturnTemplateDetails
    {
        static void Main(string[] args)
        {
            // Your username, password and customer number are imported from the following file
            // CPCWS_Returns_DotNet_Samples\REST\returns\user.xml 
            var username = ConfigurationSettings.AppSettings["username"];
            var password = ConfigurationSettings.AppSettings["password"];
            var mailedBy = ConfigurationSettings.AppSettings["customerNumber"];
            var mobo = ConfigurationSettings.AppSettings["customerNumber"];

            // REST URI
            var url = "https://ct.soa-gw.canadapost.ca/rs/" + mailedBy + "/" + mobo + "/openreturn/349641323786705649/details";

            var method = "GET"; // HTTP Method
            String responseAsString = ".NET Framework " + Environment.Version.ToString() + "\r\n\r\n";

            try
            {
                // Create REST Request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;

                // Set Basic Authentication Header using username and password variables
                string auth = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
                request.Headers = new WebHeaderCollection();
                request.Headers.Add("Authorization", auth);
                request.Headers.Add("Accept-Language", "en-CA");
                request.Accept = "application/vnd.cpc.openreturn-v2+xml";

                // Execute REST Request
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                responseAsString += "HTTP Response Status: " + (int)response.StatusCode + "\r\n\r\n";

                // Deserialize xml response to OpenReturnDetailsType object
                XmlSerializer serializer = new XmlSerializer(typeof(OpenReturnDetailsType));
                TextReader reader = new StreamReader(response.GetResponseStream());
                OpenReturnDetailsType openReturnDetailsInfo = (OpenReturnDetailsType)serializer.Deserialize(reader);

                // Retrieve values from OpenReturnDetailsType object
                responseAsString += "Artifacts Remaining: " + openReturnDetailsInfo.artifactsremaining + "\r\n";
                responseAsString += "Service Code: " + openReturnDetailsInfo.openreturn.servicecode + "\r\n";
                responseAsString += "Receiver Address Line 1: " + openReturnDetailsInfo.openreturn.receiver.domesticaddress.addressline1 + "\r\n";
                responseAsString += "Receiver Postal Code: " + openReturnDetailsInfo.openreturn.receiver.domesticaddress.postalcode + "\r\n";
            }
            catch (WebException webEx)
            {
                HttpWebResponse response = (HttpWebResponse)webEx.Response;

                if (response != null)
                {
                    responseAsString += "HTTP  Response Status: " + webEx.Message + "\r\n";

                    // Retrieve errors from messages object
                    try
                    {
                        // Deserialize xml response to messages object
                        XmlSerializer serializer = new XmlSerializer(typeof(messages));
                        TextReader reader = new StreamReader(response.GetResponseStream());
                        messages myMessages = (messages)serializer.Deserialize(reader);


                        if (myMessages.message != null)
                        {
                            foreach (var item in myMessages.message)
                            {
                                responseAsString += "Error Code: " + item.code + "\r\n";
                                responseAsString += "Error Msg: " + item.description + "\r\n";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Misc Exception
                        responseAsString += "ERROR: " + ex.Message;
                    }
                }
                else
                {
                    // Invalid Request
                    responseAsString += "ERROR: " + webEx.Message;
                }

            }
            catch (Exception ex)
            {
                // Misc Exception
                responseAsString += "ERROR: " + ex.Message;
            }

            Console.WriteLine(responseAsString);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

    }
}
