using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.CommWeb
{
    public class CommWebResponseResult
    {
        public decimal amount { get; set; }
        public string creationTime { get; set; }
        public string currency { get; set; }
        public string id { get; set; }
        public string merchant { get; set; }
        public string result { get; set; }
        public riskVm risk { get; set; }

        public sourceOfFundsVm sourceOfFunds { get; set; }


        public string status { get; set; }
        public decimal totalAuthorizedAmount { get; set; }
        public decimal totalCapturedAmount { get; set; }
        public decimal totalRefundedAmount { get; set; }
        
        public List<transactionVm> transaction { get; set; }
    }

    public class sourceOfFundsVm
    {
        public string type { get; set; }
        public providedVm provided { get; set; }
    }

    public class providedVm
    {
        public providedCardVm card { get; set; }


    }

    public class providedCardVm
    {
        public string brand { get; set; }
        public expiryVm expiry { get; set; }
        public string fundingMethod { get; set; }
        public string issuer { get; set; }
        public string number { get; set; }
        public string scheme { get; set; }
    }

    public class expiryVm
    {
        public string month { get; set; }
        public string year { get; set; }
    }

    public class transactionVm
    {
        public authorizationResponseVm authorizationResponse { get; set; }
        public string gatewayEntryPoint { get; set; }
        public string merchant { get; set; }
        public orderVm order { get; set; }
        public responseVm response { get; set; }
        public string result { get; set; }
        public riskVm risk { get; set; }

        public sourceOfFundsVm sourceOfFunds { get; set; }
        public string timeOfRecord { get; set; }
        public transactioTransactionVm transaction { get; set; }
        public string version { get; set; }
    }

    public class authorizationResponseVm
    {
        public string cardSecurityCodeError { get; set; }
        public string commercialCard { get; set; }
        public string commercialCardIndicator { get; set; }
        public string financialNetworkCode { get; set; }
        public string processingCode { get; set; }
        public string responseCode { get; set; }
        public string stan { get; set; }
        public string transactionIdentifier { get; set; }

    }
    public class orderVm
    {
        public decimal amount { get; set; }
        public string creationTime { get; set; }
        public string currency { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        public decimal totalAuthorizedAmount { get; set; }
        public decimal totalCapturedAmount { get; set; }
        public decimal totalRefundedAmount { get; set; }
    }
    public class responseVm
    {
        public string acquirerCode { get; set; }
        public string acquirerMessage { get; set; }
        public cardSecurityCodeVm cardSecurityCode { get; set; }
        public string gatewayCode { get; set; }
    }
    public class riskVm
    {
        public riskResponseVm response { get; set; }
    }
    public class transactioTransactionVm
    {
        public acquirerVm acquirer { get; set; }
        public decimal amount { get; set; }
        public string authorizationCode { get; set; }
        public string currency { get; set; }
        public string frequency { get; set; }
        public string id { get; set; }
        public string receipt { get; set; }
        public string source { get; set; }
        public string terminal { get; set; }
        public string type { get; set; }
    }

    public class cardSecurityCodeVm
    {
        public string acquirerCode { get; set; }
        public string gatewayCode { get; set; }

    }

    public class riskResponseVm
    {
        public string gatewayCode { get; set; }
        public reviewVm review { get; set; }
        public List<ruleVmLists> rule { get; set; }
    }

    public class reviewVm
    {
        public string decision { get; set; }
    }

    

    public class ruleVmLists
    {
        public string data { get; set; }
        public string name { get; set; }
        public string recommendation { get; set; }
        public string type { get; set; }
    }

    public class acquirerVm
    {

        public int batch { get; set; }
        public string date { get; set; }
        public string id { get; set; }
        public string merchantId { get; set; }
        public string transactionId { get; set; }
    }

}