using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSIPGClient.WebReference;

namespace WSIPGClient.RequestSamples.Action
{
    class InquiryOrderRequest
    {
        public IPGApiActionRequest InquiryOrderActionRequest{ get; set; }

        public InquiryOrderRequest()
        {
            InquiryOrder oInquiryOrder = new InquiryOrder();
            oInquiryOrder.StoreId = "120995000";
            oInquiryOrder.OrderId = "C-0cc41568-aef6-45bb-8b26-5d1d5733191d";

            ClientLocale oClientLocale = new ClientLocale();
            oClientLocale.Country = "UK";
            oClientLocale.Language = "en";
            
            WSIPGClient.WebReference.Action oAction;
            oAction = new WSIPGClient.WebReference.Action();
            oAction.Item = oInquiryOrder;
            oAction.ClientLocale = oClientLocale;

            InquiryOrderActionRequest = new IPGApiActionRequest();
            InquiryOrderActionRequest.Item = oAction;
        }
    }
}
