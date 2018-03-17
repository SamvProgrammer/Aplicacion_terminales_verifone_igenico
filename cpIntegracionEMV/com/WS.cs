using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Xml;
using cpIntegracionEMV.util;
using cpIntegracionEMV.data;
/*
 * ws - Web Service Client
 * lzuniga 21/06/2016.
 **/
namespace cpIntegracionEMV.com
{
    public class WS
    {
        public String SendWS(String url, String data)
        {
            try
            {
                MITLog.PrintLn("URL:"+url);
                MITLog.PrintLn("Request:" + data);

                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(url);

                //User Agent
                ((HttpWebRequest)request).UserAgent = "pcpay";
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.
                String postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                String responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
                MITLog.PrintLn("Response SendWS:" + responseFromServer);


                if (responseFromServer.Contains("<?xml version"))
                    responseFromServer = utilidadesMIT.ValidaCadena(responseFromServer);

                return responseFromServer;

            }catch( WebException ex)
            {
                MITLog.PrintLn("SendWS Exception: "+ex.ToString());
                return "";
            }
        }
        //SOAP
        public String SendWSSoap(String url, String data, String Action, String nbparams, String Method)
        {
            try
            {
                String soapxml="";
                String soapResult = "";
                data = data.Replace("<", "#");
                //Create Header
                soapxml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                          + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\""
                          +     " xmlns:q0=\"" + Action + "\""
                          +     " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\""
                          +     " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">"
                            + "<soapenv:Body>"
                                + "<q0:" + Method + ">"
                                    + "<q0:" + nbparams + ">"
                                        + data
                                    + "</q0:" + nbparams + ">"
                                + "</q0:" + Method + ">"
                            + "</soapenv:Body>"
                          + "</soapenv:Envelope>";

                MITLog.PrintLn("soapxml:" + soapxml);
                // Create a request using a URL that can receive a post. 
                //WebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //Soap action
                //request.Headers.Add(@"SOAPAction:http://reverso.ws.cobroxml.cpagos");
                request.Headers.Add("SOAPAction", Action);
                //User Agent
                //-----------((HttpWebRequest)request).UserAgent = "pcpay";
                // Create POST data and convert it to a byte array.
                String postData = soapxml;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "text/xml;charset=\"utf-8\"";
                //request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "text/xml";
                // Set the Method property of the request to POST.
                request.Method = "POST";

                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(soapxml);

                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse()) // Error occurs here
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        soapResult = soapResult.Replace("&lt;", "<");
                    }
                }
                MITLog.PrintLn("Soap Result:"+soapResult);
                return soapResult;
            }
            catch (WebException ex)
            {
                MITLog.PrintLn("SendWS Exception: " + ex.ToString());
                return "";
            }
        }

        public String SendWSSoap(String url, String data, String Action, String Method)
        {
            try
            {
                String soapxml = "";
                String soapResult = "";
                //data = data.Replace("<", "#");
                
                soapxml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                          + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\""
                          + " xmlns:q0=\"" + Action + "\""
                          + " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\""
                          + " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">"
                            + "<soapenv:Body>"
                                + "<q0:" + Method + ">"
                                        + data
                                + "</q0:" + Method + ">"
                            + "</soapenv:Body>"
                          + "</soapenv:Envelope>";

                MITLog.PrintLn("soapxml:" + soapxml);
                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", Action);
                //User Agent
                // Create POST data and convert it to a byte array.
                String postData = soapxml;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(soapxml);

                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse()) // Error occurs here
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        soapResult = soapResult.Replace("&lt;", "<");
                    }
                }
                MITLog.PrintLn("Soap Result:" + soapResult);
                return soapResult;
            }
            catch (WebException ex)
            {
                MITLog.PrintLn("SendWS Exception: " + ex.ToString());
                TRINP.DsError = ex.Response.ToString().ToString();
                return "";
            }
        }

        //GET
        public string HTTP_GET(string Url, string Data)
        {
            string Out = String.Empty;
            WebRequest req = WebRequest.Create(Url + (string.IsNullOrEmpty(Data) ? "" : "?" + Data));
            try
            {
                //non cache
                HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                req.CachePolicy = noCachePolicy;
                
                WebResponse resp = req.GetResponse();
                using (Stream stream = resp.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        Out = sr.ReadToEnd();
                        sr.Close();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Out = string.Format("HTTP_ERROR :: The second HttpWebRequest object has raised an Argument Exception as 'Connection' Property is set to 'Close' :: {0}", ex.Message);
            }
            catch (WebException ex)
            {
                Out = string.Format("HTTP_ERROR :: WebException raised! :: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Out = string.Format("HTTP_ERROR :: Exception raised! :: {0}", ex.Message);
            }
            MITLog.PrintLn("Response GET:" + Out);
            return Out;
        }


        
    }
}
