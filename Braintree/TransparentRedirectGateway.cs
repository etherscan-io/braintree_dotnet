#pragma warning disable 1591

using System;
using System.Xml;

namespace Braintree
{
    public class TransparentRedirectGateway
    {
        public const String CREATE_CUSTOMER = "create_customer";
        public const String UPDATE_CUSTOMER = "update_customer";
        public const String CREATE_PAYMENT_METHOD = "create_payment_method";
        public const String UPDATE_PAYMENT_METHOD = "update_payment_method";
        public const String CREATE_TRANSACTION = "create_transaction";

        public String Url
        {
            get { return Service.BaseMerchantURL() + "/transparent_redirect_requests"; }
        }

        private BraintreeService Service;
        private BraintreeGateway Gateway;

        protected internal TransparentRedirectGateway(BraintreeGateway gateway)
        {
            gateway.Configuration.AssertHasAccessTokenOrKeys();
            Gateway = gateway;
            Service = new BraintreeService(gateway.Configuration);
        }

        public String BuildTrData(Request request, String redirectURL)
        {
            return TrUtil.BuildTrData(request, redirectURL, Service);
        }

        public virtual Result<Transaction> ConfirmTransaction(String queryString)
        {
            TransparentRedirectRequest trRequest = new TransparentRedirectRequest(queryString, Service);
            XmlNode node = Service.Post(Service.MerchantPath() + "/transparent_redirect_requests/" + trRequest.Id + "/confirm", trRequest);

            return new ResultImpl<Transaction>(new NodeWrapper(node), Gateway);
        }

        public virtual Result<Customer> ConfirmCustomer(String queryString)
        {
            TransparentRedirectRequest trRequest = new TransparentRedirectRequest(queryString, Service);
            XmlNode node = Service.Post(Service.MerchantPath() + "/transparent_redirect_requests/" + trRequest.Id + "/confirm", trRequest);

            return new ResultImpl<Customer>(new NodeWrapper(node), Gateway);
        }

        public virtual Result<CreditCard> ConfirmCreditCard(String queryString)
        {
            TransparentRedirectRequest trRequest = new TransparentRedirectRequest(queryString, Service);
            XmlNode node = Service.Post(Service.MerchantPath() + "/transparent_redirect_requests/" + trRequest.Id + "/confirm", trRequest);

            return new ResultImpl<CreditCard>(new NodeWrapper(node), Gateway);
        }
    }
}
