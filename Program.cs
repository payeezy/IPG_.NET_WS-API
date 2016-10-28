using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using WSIPGClient.WebReference;
using System;
using System.IO;
using System.Web.Services.Protocols;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Collections.Generic;
using System.Xml;
using WSIPGClient.ExtensionMethods;
using WSIPGClient.RequestSamples.Action;
using WSIPGClient.RequestSamples.Order;
using WSIPGClient.Certificate;

namespace WSIPGClient
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            // Disable Expect100Continue, when set to true I get an error
            // The request was aborted: Could not create SSL/TLS secure channel.
            ServicePointManager.Expect100Continue = false;
            String RequestResponseMessage = "";

            //Action
            //RequestResponseMessage = doInitiateClearingActionRequest();
            //RequestResponseMessage = doInquiryOrderActionRequest();
            //RequestResponseMessage = doRecurringPaymentActionRequest();

            //Order
            RequestResponseMessage = doCreditCardTransactionOrderRequest();
            //RequestResponseMessage = doDEDirectDebitTransactionOrderRequest();

            Console.WriteLine(RequestResponseMessage);
            Console.ReadLine();
        }

        /// <summary>
        /// Method creates a IPG API Action Request, sends and recieves IPG API Action Response.
        /// </summary>
        /// <param name="oIPGApiActionRequest"></param>
        /// <returns>IPG API Action response as string</returns>
        private static String SendActionRequest(IPGApiActionRequest oIPGApiActionRequest)
        {
            var cert = CertificateHandler.LoadCertificate(@"c:\certificates\WS120995000._.4.p12", "tester02");

            IPGApiOrderService oIPGApiOrderService = new IPGApiOrderService();
            String RequestResponseMessage = "";
            if (cert != null)
            {
                oIPGApiOrderService.ClientCertificates.Add(cert);
                oIPGApiOrderService.Url = @"https://test.ipg-online.com:443/ipgapi/services";

                NetworkCredential nc = new NetworkCredential("WS120995000._.4", "Tester02");
                oIPGApiOrderService.Credentials = nc;

                //set proxy host and port
                IWebProxy webProxy = new WebProxy("host", 8080);
                webProxy.Credentials = new NetworkCredential("username", "password");
                oIPGApiOrderService.Proxy = webProxy;
                try
                {
                    //send action request and get response
                    IPGApiActionResponse oResponse = oIPGApiOrderService.IPGApiAction(oIPGApiActionRequest);
                    RequestResponseMessage = oResponse.IPGApiActionResponseToString() ?? "";
                }
                catch (SoapException se)
                {//SoapException: MerchantException or ProcessingException
                    RequestResponseMessage = se.SoapExceptionResponseToString() ?? "";
                }
                catch (Exception e)
                {
                    RequestResponseMessage = e.Message + Environment.NewLine;
                    RequestResponseMessage += e.InnerException + Environment.NewLine;
                    RequestResponseMessage += e.StackTrace;
                }
            }
            return RequestResponseMessage;
        }

        /// <summary>
        /// Method creates IPGAPI Order request, sends it and recieves IPG API Order Response
        /// </summary>
        /// <param name="oIPGApiOrderRequest"></param>
        /// <returns>IPG API Order Response as string</returns>
        private static String SendOrderRequest(IPGApiOrderRequest oIPGApiOrderRequest)
        {
            var cert = CertificateHandler.LoadCertificate(@"c:\certificates\WS120995000._.4.p12", "tester02");

            IPGApiOrderService oIPGApiOrderService = new IPGApiOrderService();
            String RequestResponseMessage = "";
            if (cert != null)
            {
                oIPGApiOrderService.ClientCertificates.Add(cert);
                oIPGApiOrderService.Url = @"https://test.ipg-online.com:443/ipgapi/services";

                NetworkCredential nc = new NetworkCredential("WS120995000._.4", "Tester02");
                oIPGApiOrderService.Credentials = nc;

                //set proxy host and port
                IWebProxy webProxy = new WebProxy("host", 8080);
                webProxy.Credentials = new NetworkCredential("username", "password");
                oIPGApiOrderService.Proxy = webProxy;
                try
                {
                    //send action request and get response
                    IPGApiOrderResponse oResponse = oIPGApiOrderService.IPGApiOrder(oIPGApiOrderRequest);
                    RequestResponseMessage = oResponse.IPGApiOrderResponseToString() ?? "";
                }
                catch (SoapException se)
                {//SoapException: MerchantException or ProcessingException
                    RequestResponseMessage = se.SoapExceptionResponseToString() ?? "";
                }
                catch (Exception e)
                {
                    RequestResponseMessage = e.Message + Environment.NewLine;
                    RequestResponseMessage += e.InnerException + Environment.NewLine;
                    RequestResponseMessage += e.StackTrace;
                }
            }
            return RequestResponseMessage;
        }

        /// <summary>
        /// Method creates an Initiate Clearing Action Request
        /// </summary>
        /// <returns></returns>
        private static String doInitiateClearingActionRequest()
        {
            //InitiateClearing
            InitiateClearingRequest oInitiateClearingRequest = new InitiateClearingRequest();
            IPGApiActionRequest oIPGApiActionRequest = oInitiateClearingRequest.InitiateClearingActionRequest;
            return SendActionRequest(oIPGApiActionRequest);
        }

        /// <summary>
        /// Method creates Inquiry Order Action Request
        /// </summary>
        /// <returns></returns>
        private static String doInquiryOrderActionRequest()
        {
            //InquiryOrder
            InquiryOrderRequest oInquiryOrderRequest = new InquiryOrderRequest();
            IPGApiActionRequest oIPGApiActionRequest = oInquiryOrderRequest.InquiryOrderActionRequest;
            return SendActionRequest(oIPGApiActionRequest);
        }

        /// <summary>
        /// Method creates Recurring Payment Action Request
        /// </summary>
        /// <returns></returns>
        private static String doRecurringPaymentActionRequest()
        {
            //RecurringPayment
            RecurringPaymentRequest oRecurringPaymentRequest = new RecurringPaymentRequest();
            IPGApiActionRequest oIPGApiActionRequest = oRecurringPaymentRequest.RecurringPaymentActionRequest;
            return SendActionRequest(oIPGApiActionRequest);
        }

        /// <summary>
        /// Method creates Credit Card Transaction Order Request
        /// </summary>
        /// <returns></returns>
        private static String doCreditCardTransactionOrderRequest()
        {
            //CreditCardTransaction
            CreditCardTransactionRequest oCreditCardTransactionOrderRequest = new CreditCardTransactionRequest();
            IPGApiOrderRequest oIPGApiOrderRequest = oCreditCardTransactionOrderRequest.CreditCardTransactionOrderRequest;
            return SendOrderRequest(oIPGApiOrderRequest);
        }

        /// <summary>
        /// Method creates DE Direct Debit Transaction Order Request
        /// </summary>
        /// <returns></returns>
        private static String doDEDirectDebitTransactionOrderRequest()
        {
            //DEDirectDebitTransaction
            DEDirectDebitTransactionRequest oDEDirectDebitTransactionRequest = new DEDirectDebitTransactionRequest();
            IPGApiOrderRequest oIPGApiOrderRequest = oDEDirectDebitTransactionRequest.DEDirectDebitTransactionOrderRequest;
            return SendOrderRequest(oIPGApiOrderRequest);
        }
    }
}