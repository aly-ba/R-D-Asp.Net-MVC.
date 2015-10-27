using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetStore.ViewModel;
using Braintree;

namespace AspNetStore.Services
{
    public class PaymentGateway : IGateway
    {
        private readonly BraintreeGateway _gateway = new BraintreeGateway
        {
            Environment = Braintree.Environment.SANDBOX,
            MerchantId = "*********************",
            PublicKey = "*********************",
            PrivateKey = "*********************"
        };




        public PaymentResult ProcessPayment(CheckoutViewModel model)
        {
            var request = new TransactionRequest()
            {
                Amount = model.Total,
               CreditCard = new TransactionCreditCardRequest()
               {
                   Number =model.CardNumber,
                   CVV=model.Cvv,
                   ExpirationYear=model.Year
               },

               Options = new  TransactionOptionsRequest() {
    
                    SubmitForSettlement = true
               }


            };
            var result = _gateway.Transaction.Sale(request);
            if (result.IsSuccess() )
            {
                return new PaymentResult(result.Target.Id, true, null);
            }
            return new PaymentResult(null, false, result.Message);
        }


    }
}