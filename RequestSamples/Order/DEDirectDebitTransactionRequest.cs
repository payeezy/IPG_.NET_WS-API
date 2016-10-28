using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSIPGClient.WebReference;

namespace WSIPGClient.RequestSamples.Order
{
    class DEDirectDebitTransactionRequest
    {
        public IPGApiOrderRequest DEDirectDebitTransactionOrderRequest { get; set; }

        public DEDirectDebitTransactionRequest()
        {
            Transaction oTransaction = new Transaction();

            DE_DirectDebitTxType oDE_DirectDebitTxType = new DE_DirectDebitTxType();
            oDE_DirectDebitTxType.StoreId = "120995000";
            oDE_DirectDebitTxType.Type = DE_DirectDebitTxTypeType.sale;

            DE_DirectDebitData oDE_DirectDebitData = new DE_DirectDebitData();
            oDE_DirectDebitData.ItemsElementName = new ItemsChoiceType2[] { ItemsChoiceType2.BIC, ItemsChoiceType2.IBAN };
            oDE_DirectDebitData.Items = new String[] { "PBNKDEFFXXX", "DE34500100600032121604" };
            oDE_DirectDebitData.MandateType = MandateType.SINGLE;
            oDE_DirectDebitData.MandateTypeSpecified = true;
            oDE_DirectDebitData.DateOfMandate = DateTime.Now.ToString("yyyyMMdd");

            oTransaction.Items = new Object[] { oDE_DirectDebitTxType, oDE_DirectDebitData };

            Payment oPayment = new Payment();
            oPayment.SubTotal = 10;
            oPayment.ValueAddedTax = 2;
            oPayment.ValueAddedTaxSpecified = true;
            oPayment.DeliveryAmount = 1;
            oPayment.DeliveryAmountSpecified = true;
            oPayment.ChargeTotal = 13;
            oPayment.Currency = "978";

            oTransaction.Payment = oPayment;

            Billing oBilling = new Billing();
            oBilling.Name = "Name";
            oBilling.Address1 = "Address";
            oBilling.City = "City";
            oBilling.State = "State";
            oBilling.Zip = "Zip";
            oBilling.Country = "Country";

            oTransaction.Billing = oBilling;

            ClientLocale oClientLocale = new ClientLocale();
            oClientLocale.Country = "UK";
            oClientLocale.Language = "en";

            oTransaction.ClientLocale = oClientLocale;

            DEDirectDebitTransactionOrderRequest = new IPGApiOrderRequest();
            DEDirectDebitTransactionOrderRequest.Item = oTransaction;
        }
    }
}
