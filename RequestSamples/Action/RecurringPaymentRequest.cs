using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSIPGClient.WebReference;

namespace WSIPGClient.RequestSamples.Action
{
    class RecurringPaymentRequest
    {
        public IPGApiActionRequest RecurringPaymentActionRequest { get; set; }

        public RecurringPaymentRequest()
        {
            RecurringPayment oRecurringPayment = new RecurringPayment();
            oRecurringPayment.StoreId = "120995000";
            oRecurringPayment.Function = Function.install;

            RecurringPaymentInformation oRecurringPaymentInformation = new RecurringPaymentInformation();
            oRecurringPaymentInformation.InstallmentCount = "12";
            oRecurringPaymentInformation.InstallmentFrequency = "1";
            oRecurringPaymentInformation.InstallmentPeriod = RecurringPaymentInformationInstallmentPeriod.month;
            oRecurringPaymentInformation.InstallmentPeriodSpecified = true;
            DateTime dateNow = DateTime.Now.AddDays(5);
            oRecurringPaymentInformation.RecurringStartDate = dateNow.ToString("yyyyMMdd");
            oRecurringPaymentInformation.MaximumFailures = "3";
            
            oRecurringPayment.RecurringPaymentInformation = oRecurringPaymentInformation;

            CreditCardData oCreditCardData = new CreditCardData();
            oCreditCardData.Brand = CreditCardDataBrand.VISA;
            oCreditCardData.BrandSpecified = true;
            oCreditCardData.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.CardNumber, ItemsChoiceType.ExpMonth, ItemsChoiceType.ExpYear, ItemsChoiceType.CardCodeValue };
            oCreditCardData.Items = new Object[] { "4035874000424977", "12", "18", "977" };

            oRecurringPayment.CreditCardData = oCreditCardData;

            Payment oPayment = new Payment();
            oPayment.SubTotal = 13;
            oPayment.ChargeTotal = 13;
            oPayment.Currency = "978";

            oRecurringPayment.Payment = oPayment;

            Billing oBilling = new Billing();
            oBilling.Name = "Name";
            oBilling.Address1 = "Address";
            oBilling.City = "City";
            oBilling.State = "State";
            oBilling.Zip = "Zip";
            oBilling.Country = "Country";

            oRecurringPayment.Billing = oBilling;

            ClientLocale oClientLocale = new ClientLocale();
            oClientLocale.Country = "UK";
            oClientLocale.Language = "en";
            
            WSIPGClient.WebReference.Action oAction;
            oAction = new WSIPGClient.WebReference.Action();
            oAction.Item = oRecurringPayment;
            oAction.ClientLocale = oClientLocale;

            RecurringPaymentActionRequest = new IPGApiActionRequest();
            RecurringPaymentActionRequest.Item = oAction;
        }
    }
}
