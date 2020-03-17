using Aliyun.Acs.Alidns.Model.V20150109;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Aliyun.Acs.Alidns.Model.V20150109.DescribeDomainRecordsResponse;
using static Aliyun.Acs.Alidns.Model.V20150109.DescribeDomainsResponse;

namespace LinkHome
{
    

    class AliDDnsUtils
    {
        public string AccessKeyID;
        public string AccessKeySecret;
        public int TTL;
        public ErrorHandler ErrorHandler;

        T getResponse<T>(AcsRequest<T> request) where T : AcsResponse
        {
            while (true)
            {
                try
                {
                    var profile = DefaultProfile.GetProfile(
                        "cn-qingdao",
                        AccessKeyID,
                        AccessKeySecret
                    );
                    var client = new DefaultAcsClient(profile);
                    return client.GetAcsResponse(request);
                }
                catch (Exception ex)
                {
                    ErrorHandler(ex.Message); 
                } 
                Thread.Sleep(10000);
            }
        }

        public List<DescribeDomains_Domain> GetDomains()
        {
            return getResponse(new DescribeDomainsRequest()).Domains;
        } 


        public List<DescribeDomainRecords_Record> GetRecords(DescribeDomains_Domain domain)
        {
            var req = new DescribeDomainRecordsRequest();
            req.DomainName = domain.DomainName;
            return getResponse(req).DomainRecords;
        }

        public void UpdateRecordIP(DescribeDomainRecords_Record record, string ip)
        {
            getResponse(new UpdateDomainRecordRequest
            {
                RR = record.RR,
                RecordId = record.RecordId,
                _Value = ip,
                Type = "A",
                TTL = TTL,
            });
        }

        public void DeleteRecord(string recordID)
        {
            getResponse(new DeleteDomainRecordRequest { RecordId = recordID });
        } 

        public void SetupRecord(string domain, string rrKeyword, string newValue)
        {
            var reqRecords = new DescribeDomainRecordsRequest();
            reqRecords.DomainName = domain;
            reqRecords.RRKeyWord = rrKeyword;
            reqRecords.Type = "A";
            var records = getResponse(reqRecords).DomainRecords;

            if (records.Count == 0)
            {
                var add = new AddDomainRecordRequest
                {
                    RR = rrKeyword,
                    _Value = newValue,
                    Type = "A",
                    TTL = TTL,
                    DomainName = domain
                };
                getResponse(add);
                return;
            }

            var record = records[0];

            if (newValue == record._Value
                && record.RR == rrKeyword
                && record.Type == "A"
                && record.TTL == TTL)//解析值没有变化则跳出
            {
                return;
            }

            var reqUpdate = new UpdateDomainRecordRequest();
            reqUpdate.RR = rrKeyword;
            reqUpdate.RecordId = record.RecordId;
            reqUpdate._Value = newValue;
            reqUpdate.Type = "A";
            reqUpdate.TTL = TTL;
            getResponse(reqUpdate);
        }

    }
}
