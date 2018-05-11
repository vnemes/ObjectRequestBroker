using System;
using System.Collections.Generic;
using System.Net;
using ORB.orbapi;
using ORB.requestreplyapi.entries;
using ORB.requestreplyapi.requestreply;

namespace ORB.namingservice
{
    using Marshaller = Marshaller;
    using Entry = Entry;
    using ExtendedEntry = ExtendedEntry;
    using Replyer = Replyer;
    using InvalidRequestReply = global::ORB.namingservice.replies.InvalidRequestReply;
    using LocalizationReply = global::ORB.namingservice.replies.LocalizationReply;
    using RegistrationReply = global::ORB.namingservice.replies.RegistrationReply;
    using Reply = global::namingservice.replies.Reply;
    using LocalizationRequest = global::ORB.namingservice.requests.LocalizationRequest;
    using RegistrationRequest = global::ORB.namingservice.requests.RegistrationRequest;


    public class NamingService
    {
        private static readonly NamingService _instance = new NamingService();
        public static Entry NAMING_SERVICE_ENTRY;

        static NamingService()
        {
            NAMING_SERVICE_ENTRY = new Entry(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), 1111);
        }

        private const string NAMING_SERVICE_NAME = "NamingService";
        private Dictionary<string, ExtendedEntry> _entryMap;
        private bool _isServiceRunning;

        public static NamingService Instance
        {
            get { return _instance; }
        }

        private NamingService()
        {
            _entryMap = new Dictionary<string, ExtendedEntry>();
            _isServiceRunning = false;
        }

        public void StartService()
        {
            if (_isServiceRunning)
            {
                Console.WriteLine("NamingService already running");
                return;
            }

            var mReplyer = new Replyer(NAMING_SERVICE_NAME, NAMING_SERVICE_ENTRY);
            _isServiceRunning = true;

            while (_isServiceRunning)
            {
                mReplyer.receive_transform_and_send_feedback(new TransformerFunc((@in) =>
                {
                    Reply reply;
                    var inStr = Marshaller.StringFromByteArray(@in);
                    if (inStr.Contains("registration_request"))
                    {
                        reply = ProcessRegistrationRequest(@in);
                    }
                    else if (inStr.Contains("localization_request"))
                    {
                        reply = ProcessLocalizationRequest(@in);
                    }
                    else
                    {
                        reply = new InvalidRequestReply();
                    }

                    return Marshaller.MarshallObject(reply);
                }));
            }
        }

        private Reply ProcessLocalizationRequest(byte[] @in)
        {
            var locRequest = (LocalizationRequest) Marshaller.UnMarshallObject(@in);

            return new LocalizationReply("localization_reply", _entryMap[locRequest.EntryName],
                _entryMap[locRequest.EntryName] != null);
        }

        private Reply ProcessRegistrationRequest(byte[] @in)
        {
            var regRequest = (RegistrationRequest) Marshaller.UnMarshallObject(@in);
            bool req_res;
            if (_entryMap.ContainsKey(regRequest.EntryName))
            {
                req_res = false;
            }
            else
            {
                _entryMap[regRequest.EntryName] = regRequest.EntryData;
                req_res = true;
            }

            return new RegistrationReply("registration_reply", req_res);
        }

        public void StopService()
        {
            if (!_isServiceRunning)
            {
                Console.WriteLine("NamingService is not running!");
                return;
            }

            _isServiceRunning = false;
        }
    }
}